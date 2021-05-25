; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "OrianaLauncher"
#define MyAppVersion "1.0"
#define MyAppPublisher "Oriana"
#define MyAppURL "https://www.team-oriana.fr"
#define MyAppExeName "OrianaLauncher.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{6F4C6956-ABF7-46EB-9A97-72477ACF335B}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=D:\visualstudio\OrianaLauncher\output
OutputBaseFilename=OrianaSetup
SetupIconFile=D:\visualstudio\OrianaLauncher\OrianaLauncher\Assets\logo_Oriana.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\visualstudio\OrianaLauncher\OrianaLauncher\bin\Release\net5.0-windows\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\OrianaLauncher\OrianaLauncher\bin\Release\net5.0-windows\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\OrianaLauncher\OrianaLauncher\bin\Release\net5.0-windows\Octokit.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\OrianaLauncher\OrianaLauncher\bin\Release\net5.0-windows\OrianaLauncher.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\OrianaLauncher\OrianaLauncher\bin\Release\net5.0-windows\OrianaLauncher.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\OrianaLauncher\OrianaLauncher\bin\Release\net5.0-windows\OrianaLauncher.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\OrianaLauncher\OrianaLauncher\bin\Release\net5.0-windows\OrianaLauncher.runtimeconfig.dev.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\OrianaLauncher\OrianaLauncher\bin\Release\net5.0-windows\OrianaLauncher.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\OrianaLauncher\OrianaLauncher\token.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\OrianaLauncher\OrianaLauncher\bin\Release\net5.0-windows\ref\*"; DestDir: "{app}\ref"; Flags: ignoreversion recursesubdirs createallsubdirs

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
