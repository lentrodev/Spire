# Telegram Bot Builder framework.

## Table of contents

- [Overview](#overview)
  - [Framework structure](#framework-structure)
  - [Available packages](#available-packages)
- [Getting Started](#getting-started)
    
## Overview

**Spire** - fast, easy-in-use, module-based Telegram Bot framework for .Net applications. It uses reflection for registering and invoking update handlers and commands. 

### Framework structure

<details>
  <summary>Show framework structure</summary>


- [**Spire**](#spire)
- **Core**
  - [**Spire.Core**](#spirecore) 
  - [**Spire.Core.Abstractions**](#spirecoreabstractions) 
  - **Commands**
    - [**Spire.Core.Commands**](#spirecorecommands)
    - [**Spire.Core.Commands.Abstractions**](#spirecorecommandsabstractions)
    - **Parsing**
      - [**Spire.Commands.Parsing**](#spirecorecommandsparsing)
      - [**Spire.Commands.Parsing.Abstractions**](#spirecorecommandsparsingabstractions)
  - **Markups**
    - [**Spire.Core.Markups**](#spirecoremarkups)
    - [**Spire.Core.Markups.Abstractions**](#spirecoremarkupsabstractions)
  - **Sessions**
    - [**Spire.Core.Sessions**](#spirecoresessions)
    - [**Spire.Core.Sessions.Abstractions**](#spirecoresessionsabstractions)
- **Hosting**
  - [**Spire.Hosting**](#spirehosting)
  - [**Spire.Hosting.Console**](#spirehostingconsole)

</details>

### Available packages

<details>
  <summary>Show framework packages descriptions</summary>

#### Spire

Library which contains all needed classes for building Telegram bot. Minimal implementation of [**Spire.Core**](#spirecore) and [**Spire.Core.Abstractions**](#spirecoreabstractions) 

#### Spire.Core

Core library, contains base classes for building Telegram bot.

#### Spire.Core.Abstractions

Abstractions for [**Spire.Core**](#spirecore) package.

#### Spire.Core.Commands

Enables support for creating and handling commands.

#### Spire.Core.Commands.Abstractions

Abstractions for [**Spire.Core.Commands**](#spirecorecommands) package.

#### Spire.Core.Commands.Parsing

Enables support for customizable command arguments parsing.

#### Spire.Core.Commands.Parsing.Abstractions

Abstractions for [**Spire.Core.Commands**](#spirecorecommands) package.

#### Spire.Core.Markups

Adds markup builders (InlineKeyboardMarkupBuilder and ReplyKeyboardMarkupBuilder).

#### Spire.Core.Markups.Abstractions

Abstractions for [**Spire.Core.Markups**](#spirecoremarkups) package.

#### Spire.Core.Sessions

Enables user sessions support.

#### Spire.Core.Sessions.Abstractions

Abstractions for [**Spire.Core.Sessions**](#spirecoresessions) package.

#### Spire.Hosting.Console

Contains base classes for bot deployment as a console application.

#### Spire.Hosting

Contains base classes for bot deployment in any workspace.

</details>

## Getting Started

Check project's [examples](https://github.com/avalanche759/spire/tree/main/examples) folder to see how it works. It shows basic framework functionality:
- Creating typed update handlers (E.g Message handlers, CallbackQuery handlers).
- Creating and handling typed commands with argument parsing.
- Interactivity with users (replacement for state-machine mechanism). E.g filling some form, etc.

> Documentation is still in progress...