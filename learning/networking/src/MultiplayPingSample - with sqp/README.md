# Multiplay Ping Sample

## About
The *Multiplay Ping Sample* sample provides a sample implentation of various features to enable compatibility with Multiplay's cloud-hosted dedicated server solution and the Multiplay Matchmaker.

In this sample, "game traffic" is simulated by sending UDP pings to and from a server.  Additional functionality is included to demonstrate Multplay compatibility and features:

### Server Features:
* SQP (Server Query Protocol) example
    * Health check, minimal server state metrics
* Session service example
    * For reading session data passed in from the Matchmaker when a server is allocated for a match
* IP and port binding
* Config file reading

### Client Features:
* Multiplay QoS example
    * Enable pinging for available server regions and attaching that information to matchmaking requests
* Multiplay Matchmaking example
    * Enable sending matchmaking requests via the Multiplay Matchmaking service

## Building and using the sample
The client and server can be built into standalone players, but can also both be run directly from the editor.

To run in the Editor, just load up the correct scene and enter play mode.

    Note: If you run the server in the editor, it will be initialized with the values set on the MultiplayPingServer GameObject

### Unity Editor versions validated for use with this example
* 2019.1.1
    * This is the default version of the project
* 2019.1.14
    * The default Burst package (`v1.0.4`) will need to be updated to `v1.1.1` to enable the project to compile on 2019.1.14

### Building the Multiplay Ping Server standalone executable
The server can be built using the Unity Editor's build window:
1. Open the Build Settings window
2. Ensure that the `MultiplayPingServer` scene is included and the only selected scene
3. Select a supported platform (Mac x64, Windows x64, Linux x64)
4. Ensure that the `Server Build` tickbox is ticked
5. Press the `Build` button

### Building the Multiplay Ping Client standalone executable
The client can be built using the Unity Editor's build window:
1. Open the Build Settings window
2. Ensure that the `MultiplayPingClient` scene is included and the only selected scene
3. Select a platform
4. Ensure that the `Server Build` tickbox is NOT ticked
5. Press the `Build` button

## Usage

### Server command-line arguments
|Argument|Effect|Default|
|---|---|---|
|`-nographics` and `-batchmode` together|Enable headless / command-line mode|Disabled|
|`-logfile <path>`|Redirect all log output to `<path>`.  Will not print debug to console/STDOUT if enabled.|Disabled|
|`-fps <value>`|Set the target FPS (FPS the server will *attempt* to run at)|120|
|`-ip <value>`|IP address to use (bind to) |127.0.0.1|
|`-port <value>`|Port to use (bind to) for ping traffic between client and server|9000|
|`-query_port <value>`|Port to use for SQP (Server Query Protocol) traffic between the server and Multiplay|9010|
|`-config <path>`|Path to multiplay config data populated on the DGS (allocation UUID, etc.)|Disabled|
|`-serverconfig <path>`|Path to json config file used to initialize some server settings.  Will use built-in defaults if not specified.|Disabled|
|`-exampleconfig <path>`|Print example json config file to `<path>`|Disabled|
|`-timeout <value>`|Set the time (in seconds) the server must wait before automatically shutting down due to no activity (0 = infinite)|600 (10 minutes)|

**Special notes:**
* The **server** standalone **will not** show a GUI if launched normally, and should always be run in command-line mode
* The values for these can be set in many ways, but there is an order in which various settings will be overridden:
    * From lowest to highest priority:
        * Defaults
        * Config object passed on server construction
        * Config object loaded through `-config` arguments
        * All command-line arguments (`-ip`, `-port`, etc.) other than `-config`
* An example config file will be written to ExampleConfig.cfg on server startup if one doesn't already exist and the `-config` argument was not used
* While the config file includes `CurrentPlayers` as a value, it will be overridden at runtime by the number of connected ping clients

### Client command-line arguments
|Argument|Effect|Default|
|---|---|---|
|`-nographics` and `-batchmode` together|Enable headless / command-line mode|Disabled|
|`-logfile <path>`|Redirect all log output to `<path>`.  Will not print debug to console/STDOUT if enabled.|Disabled|
|`-fps <value>`|Set the target FPS (FPS the client will *attempt* to run at)|60|
|`-mm <value>`|Tell the client to use the provided **matchmaker URI** (`value`) and attempt to use matchmaking to connect to a server|Disabled|
|`-ping` / `-p`|Tell the client to connect to and ping a server|False|
|`-kill` / `-k`|Tell the client to connect to a server and send a remote shutdown signal.|False|
|`-endpoint <value>` / `-e <value>`|Tell the client which server to connect to.  `<value>` must be a valid IP address and port in the form `IPaddress:Port`.|None|
|`-t <value>`|The amount of time (ms) to spend pinging a server (requires `-p`)|5000 (5 seconds)|
|`-fleet <value>`|Enables QoS when using matchmaking; will try to ping qos servers for provided multiplay fleet ID|Disabled|

**Special notes:**
* The **client** standalone player supports both GUI mode (if launched normally) and command-line mode
    * GUI mode will allow you to perform multiple operations against multiple servers repeatedly
    * Commandline mode will execute a specific task (specified by args) and then exit
* Commandline mode:
    * The client will print ping statistics and close after completing all tasks
    * The following arguments are *required* in commandline mode:
        * `-endpoint` or `-mm` (You need to specify how to connect to a server)
        * `-ping` and/or `-kill` (You must specicy what to do)
    * If you provide both `-ping` and `-kill` as arguments, the client will do the following:
        1. Connect to the server (specified by `-endpoint` or `-mm`)
        2. Ping for the duration set by `-t` (or use the default if not set)
        3. Send remote disconnect to the server

## Thrid-Party Dependencies
* Protobuf
    * The ping *client* has a dependency on Protobuf to do client-service communications
    * The versions of Protobuf included in this repo are pulled from the gRPC Unity experimental builds here: https://github.com/grpc/grpc/tree/master/src/csharp/experimental
* Json .NET
    * The ping *server* has a dependency on Json .NET to do server-service communications
    * The version used in this sample is from https://assetstore.unity.com/packages/tools/input-management/json-net-for-unity-11347