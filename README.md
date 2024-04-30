# WSL2 Configuration Editor

WSL2 Configuration Editor is a tool for modifying the WSL2 configuation without manualy editing the .wslconfig file. It provides safe agaist setting .wslconfig file with invalid parameters as well as verifies the supported settings for the Windows version. This tool comes as a command line tool by default. But the modular design allows the core functionalities to be used through a GUI. Both Core Library and Command Line Interface target .NET 8.

### Table of Contents
- [Building](#build)
- [Dependencies](#deps)
- [CLI Commands](#commands)
- [Core Library](#core-lib)
- [License](#license)

<a name="build"></a>
### Building
- Clone the repository: `https://github.com/YashojaLakmith/WSL2ConfigurationEditor.git`
- Build the solution with .NET 8 SDK.

<a name="deps"></a>
### Dependencies
- .NET 8 SDK must be present in order to build the apllication as either a framework-dependent or a self-contained application.
- .NET 8 Runtime is required for running the CLI application as a framework-dependent application.

<a name="commands"></a>
### CLI Commands
| Command						| Description													|
| :---							| :---															|
| `help`						| Get help														|
| `about`						| About the application											|
| `exit`						| Exit the application											|
| `list-settings`				| Lists all the settings										|
| `info <setting key>`			| Shows deteiled information about the setting with given key.	|
| `set <setting key> <value>`	| Sets the given setting to the given value.					|
| `save-changes`				| Saves the changes to the .wslconfig file.						|
| `refresh`						| Reloads the configuration from the .wslconfig file.			|
| `reset-changes`				| Resets all unsaved changes to their current value.			|
| `make-default <setting key>`	| Sets the given setting to its default value.					|
| `make-default-all`			| Sets all settings to their default values.					|

<a name="core-lib"></a>
### Core Library
The [Core](https://github.com/YashojaLakmith/WSL2ConfigurationEditor/tree/master/Core) Library contains all the logic for interacting with file systems, operating system services, implementations of all the available settings and their internal representations as well as APIs for interacting with them. Therefore, it can be referenced by any other project such as a GUI.

<a name="license"></a>
### License
WSL Configuration Editor is an Open Source Software license under the [MIT License](#LICENSE.txt).