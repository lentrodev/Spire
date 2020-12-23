#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitHub;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

#endregion

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
[GitHubActions("development", GitHubActionsImage.WindowsLatest,
    ImportGitHubTokenAs = "GitHubToken",
    InvokedTargets = new[] {nameof(Compile)},
    OnPushBranches = new[] {"develop"}
)]
[GitHubActions("release", GitHubActionsImage.WindowsLatest,
    ImportGitHubTokenAs = "GitHubToken",
    ImportSecrets = new[] {nameof(NuGetApiToken)},
    InvokedTargets = new[] {nameof(Publish)},
    OnPushBranches = new[] {"main"},
    PublishArtifacts = true
)]
class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    const string Version = "0.11.0";

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    AbsolutePath PackagesDirectory => ArtifactsDirectory / "nuget";

    [Parameter] readonly string NuGetApiToken;

    Target Clean => _ => _
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
            EnsureCleanDirectory(PackagesDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetVersion(Version)
                .EnableNoRestore());
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .Produces(PackagesDirectory)
        .Executes(() =>
        {
            string repositoryOwner = GitRepository.GetGitHubOwner();

            string repositoryUrl =
                $"https://github.com/{GitRepository.GetGitHubOwner()}/{GitRepository.GetGitHubName()}";

            string readmeUrl =
                GitRepository.GetGitHubBrowseUrl("README.md");

            string licenceUrl =
                GitRepository.GetGitHubBrowseUrl("LICENSE.md");

            Solution.GetProjects("*")
                .Where(x => !Regex.IsMatch(x.Name, "(Example|Build)"))
                .ForEach(
                    project
                        => DotNetPack(_ => _
                            .SetProject(project)
                            .SetConfiguration(Configuration)
                            .SetOutputDirectory(PackagesDirectory)
                            .EnableNoRestore()
                            .SetAuthors(repositoryOwner)
                            .SetCopyright("Copyright © Mikhail Kozlov")
                            .SetPackageTags(new[] {"telegram", "bot"}.Concat(project.Name.ToLower().Split('.')))
                            .SetDescription(
                                $"Package description here: {readmeUrl}#{project.Name.ToLower().Replace(".", "")}")
                            .SetPackageLicenseUrl(licenceUrl)
                            .SetRepositoryType("git")
                            .SetRepositoryUrl(repositoryUrl)
                            .SetVersion(Version)
                        )
                );
        });

    Target Publish => _ => _
        .DependsOn(Pack)
        .Consumes(Pack)
        .Executes(() =>
        {
            PackagesDirectory
                .GlobFiles("*.nupkg")
                .ForEach(
                    package
                        => DotNetNuGetPush(_ => _
                            .SetTargetPath(package)
                            .SetSource("https://api.nuget.org/v3/index.json")
                            .SetApiKey(NuGetApiToken)));
        });

}