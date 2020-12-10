#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Spire.Core.Abstractions.Processing;

#endregion

namespace Spire.Core.Extensions
{
    public static class ReflectionExtensions
    {
        public static async ValueTask<bool> InvokeHandlerSafe<TAttribute>(
            this IActivatedDescriptor<TAttribute> activatedDescriptor,
            IContainer serviceContainer,
            IEnumerable<Type> allowedReturnTypes,
            params object[] availableArguments)
            where TAttribute : Attribute
        {
            Type handlerMethodReturnType = activatedDescriptor.Method.ReturnType;

            object[] methodArguments =
                activatedDescriptor.Method.GetParameters()
                    .RetrieveParametersValues(serviceContainer, availableArguments);

            bool returnTypeIsValid = true;

            if (!allowedReturnTypes.Any(x => x == handlerMethodReturnType))
            {
                if (allowedReturnTypes.Any())
                    returnTypeIsValid = false;
            }

            if (returnTypeIsValid)
            {
                if (handlerMethodReturnType == typeof(bool))
                {
                    return (bool) activatedDescriptor.Method.Invoke(
                        activatedDescriptor.DeclaringTypeInstance, methodArguments)!;
                }
                else if (handlerMethodReturnType == typeof(Task) || handlerMethodReturnType == typeof(ValueTask))
                {
                    await activatedDescriptor.Method.InvokeAsyncSafe(activatedDescriptor.DeclaringTypeInstance,
                        methodArguments);
                    return true;
                }
                else
                    return await activatedDescriptor.Method.InvokeAsyncSafe<bool>(
                        activatedDescriptor.DeclaringTypeInstance, methodArguments);
            }
            else
                throw new NotSupportedException(
                    "Handler method should have ValueTask<bool> or Task<bool> or bool return type.");
        }

        public static ValueTask<T> InvokeAsyncSafe<T>(this MethodInfo methodInfo, object target,
            params object[] arguments)
        {
            var invocationResult = methodInfo.Invoke(target, arguments);

            if (invocationResult is Task<T> task)
            {
                return new ValueTask<T>(task);
            }
            else if (invocationResult is ValueTask<T> valueTask)
            {
                return valueTask;
            }

            throw new Exception("Target method should have ValueTask<T> return type.");
        }


        public static ValueTask InvokeAsyncSafe(this MethodInfo methodInfo, object target, params object[] arguments)
        {
            var invocationResult = methodInfo.Invoke(target, arguments);

            if (invocationResult is Task task)
            {
                return new ValueTask(task);
            }
            else if (invocationResult is ValueTask valueTask)
            {
                return valueTask;
            }

            throw new Exception("Target method should have awaitable return type.");
        }

        public static bool TryActivateType(this Type type, IContainer serviceContainer, out object activatedInstance)
        {
            if (type == null)
            {
                activatedInstance = null;
                return true;
            }

            ConstructorInfo[] allConstructors = type.GetConstructors();

            foreach (ConstructorInfo constructor in allConstructors)
            {
                object[] parametersValues = constructor.GetParameters().RetrieveParametersValues(serviceContainer);

                try
                {
                    object activatedTypeInstance = constructor.Invoke(parametersValues);

                    activatedInstance = activatedTypeInstance;

                    return true;
                }
                catch
                {
                }
            }

            activatedInstance = null;
            return false;
        }

        public static object[] RetrieveParametersValues(this ParameterInfo[] parameters, IContainer serviceContainer,
            params object[] availableArguments)
        {
            object[] arguments = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameterInfo = parameters[i];

                Type parameterType = parameterInfo.ParameterType;

                ICollection<Parameter> resolvingParameters = new Collection<Parameter>();

                foreach (Attribute attribute in parameterInfo.GetCustomAttributes())
                {
                    resolvingParameters.Add(new TypedParameter(attribute.GetType(), attribute));
                }

                object argumentValue =
                    serviceContainer.ResolveOptional(parameterInfo.ParameterType, resolvingParameters);

                if (argumentValue == null)
                {
                    foreach (object arg in availableArguments)
                    {
                        Type argType = arg.GetType();

                        if (argType.GetInterface(parameterType.Name) != null ||
                            parameterType.GetInterface(argType.Name) != null)
                        {
                            argumentValue = arg;
                        }
                    }
                }

                arguments[i] = argumentValue;
            }

            return arguments;
        }

        public static IEnumerable<TActivatedDescriptor> ActivateDescriptors<TDescriptor, TActivatedDescriptor,
            TAttribute>(
            this IEnumerable<TDescriptor> descriptors,
            IContainer serviceContainer,
            Func<TDescriptor, object, TActivatedDescriptor> descriptorConverter)
            where TDescriptor : IDescriptor<TAttribute>
            where TActivatedDescriptor : IActivatedDescriptor<TAttribute>
            where TAttribute : Attribute
        {
            foreach (IGrouping<Type, TDescriptor> descriptorGrouping in
                descriptors.GroupBy(x => x.DeclaringType))
            {
                Type descriptorType = descriptorGrouping.Key;

                descriptorType.TryActivateType(serviceContainer, out object descriptorTypeInstance);

                foreach (TDescriptor descriptor in descriptorGrouping)
                {
                    if (descriptor is TActivatedDescriptor alreadyActivated)
                    {
                        yield return alreadyActivated;
                    }
                    else
                    {
                        yield return descriptorConverter(descriptor, descriptorTypeInstance);
                    }
                }
            }
        }

        public static bool TryCreateDescriptor<TDescriptor, TRequiredAttribute>(
            this MethodInfo methodInfo,
            IEnumerable<Type> allowedReturnTypes,
            Func<TRequiredAttribute, bool> attributeMatcher,
            Func<MethodInfo, TRequiredAttribute, TDescriptor> descriptorFactory,
            out TDescriptor descriptor)
            where TDescriptor : IDescriptor<TRequiredAttribute>
            where TRequiredAttribute : Attribute
        {
            IEnumerable<TRequiredAttribute> attributes =
                methodInfo.GetCustomAttributes<TRequiredAttribute>();

            TRequiredAttribute foundRequiredAttribute = null;

            foreach (TRequiredAttribute attribute in attributes)
            {
                if (attributeMatcher(attribute))
                {
                    foundRequiredAttribute = attribute;
                }
            }

            Type methodReturnType = methodInfo.ReturnType;

            if (foundRequiredAttribute == null || allowedReturnTypes.All(returnType => returnType != methodReturnType))
            {
                descriptor = default;
                return false;
            }

            descriptor = descriptorFactory(methodInfo, foundRequiredAttribute);
            return true;
        }
    }
}