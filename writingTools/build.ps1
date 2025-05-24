# build.ps1 - Professional build and management script for a WPF project with detailed logging (no colors)

Clear-Host

# Define log file path
$logFile = Join-Path -Path (Get-Location) -ChildPath "build.log"

# Function to write both to console and log file without colors
function Write-Log {
    param(
        [string]$Message
    )
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    $logMessage = "$timestamp | $Message"
    # Write to console (no color)
    Write-Host $Message
    # Append to log file
    Add-Content -Path $logFile -Value $logMessage
}

# Function to log section headers
function Write-Section {
    param([string]$Title)
    $line = "════════════════════════════════════════════════════════════════════════"
    Write-Log ""
    Write-Log "🔹🔹🔹 $Title 🔹🔹🔹"
    Write-Log $line
}

# Function to log errors
function Write-ErrorLog {
    param([string]$Message)
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    $errorMessage = "$timestamp | ERROR: $Message"
    Write-Host "ERROR: $Message"
    Add-Content -Path $logFile -Value $errorMessage
}

# Initialize log file with header
$header = @"
════════════════════════════════════════════════════════════════════════
Build Script Log - $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')
Directory: $(Get-Location)
PowerShell Version: $($PSVersionTable.PSVersion)
OS Version: $([Environment]::OSVersion.VersionString)
════════════════════════════════════════════════════════════════════════
"@

Set-Content -Path $logFile -Value $header

Write-Log "Starting full app build process..."

try {
    # SECTION 1: .NET SDK Check
    Write-Section "Checking for required .NET SDK"
    $dotnet = Get-Command dotnet -ErrorAction SilentlyContinue
    if (-not $dotnet) {
        Write-ErrorLog " .NET SDK not found! Please install it: https://dotnet.microsoft.com/en-us/download"
        exit 1
    }
    else {
        $version = dotnet --version
        Write-Log "INFO: .NET SDK found: $version"
    }

    # SECTION 2: CLEAN BUILD FOLDERS
    Write-Section "Cleaning build directories"
    $cleanStart = Get-Date
    @("bin", "obj") | ForEach-Object {
        $folder = $_
        if (Test-Path $folder) {
            # Log contents before deletion
            $files = Get-ChildItem -Path $folder -Recurse -Force | Select-Object FullName
            if ($files.Count -gt 0) {
                Write-Log "Contents of $folder before deletion:"
                foreach ($f in $files) {
                    Write-Log " - $($f.FullName)"
                }
            } else {
                Write-Log "$folder folder is empty before deletion"
            }
            # Delete folder
            Remove-Item -Recurse -Force $folder
            Write-Log "Removed $folder folder"
        } else {
            Write-Log "$folder folder does not exist, skipping..."
        }
    }
    $cleanEnd = Get-Date
    $cleanDuration = ($cleanEnd - $cleanStart).TotalSeconds
    Write-Log "Cleaning completed in $cleanDuration seconds"

   # SECTION 3: BUILD PROJECT
        Write-Section "Building the project"
        $buildStart = Get-Date
        
        # Automatically select a solution or project file to build
        $projectFile = $null
        $solutionFiles = Get-ChildItem -Filter *.sln
        $projectFiles = Get-ChildItem -Filter *.csproj
        
        if ($solutionFiles.Count -eq 1) {
            $projectFile = $solutionFiles[0].FullName
            Write-Log "Detected solution file: $projectFile"
        } elseif ($projectFiles.Count -eq 1) {
            $projectFile = $projectFiles[0].FullName
            Write-Log "Detected project file: $projectFile"
        } else {
            Write-ErrorLog "ERROR: Multiple or no .sln/.csproj files found. Please specify the project file manually in the script."
            exit 1
        }
        
        Write-Log "Running 'dotnet build $projectFile' command..."
        $buildOutput = dotnet build $projectFile 2>&1 | Tee-Object -Variable buildOutputVar
        $buildExitCode = $LASTEXITCODE
        Write-Log "dotnet build exit code: $buildExitCode"
        
        if ($buildExitCode -eq 0) {
            Write-Log "Build succeeded!"
            Write-Log "Build output:"
            foreach ($line in $buildOutputVar) {
                Write-Log " > $line"
            }
        } else {
            Write-ErrorLog "Build failed! Output:"
            foreach ($line in $buildOutputVar) {
                Write-ErrorLog " > $line"
            }
            exit 1
        }
        $buildEnd = Get-Date
        $buildDuration = ($buildEnd - $buildStart).TotalSeconds
        Write-Log "Build completed in $buildDuration seconds"


    # SECTION 4: RUN THE APP
    Write-Section "Attempting to run the application"
    $outputPath = Join-Path -Path (Get-Location) -ChildPath "bin\Debug\net8.0-windows"
    Write-Log "Looking for executable in $outputPath"

    if (-Not (Test-Path $outputPath)) {
        Write-ErrorLog "Build output directory does not exist: $outputPath"
        exit 1
    }

    $appExe = Get-ChildItem -Path $outputPath -Filter "*.exe" | Where-Object {
        $_.Name -notlike "*vshost*" -and $_.Name -ne "AppHost.exe"
    } | Select-Object -First 1

    if ($appExe) {
        Write-Log "Found executable: $($appExe.FullName)"
        Write-Log "Launching application..."
        Start-Process $appExe.FullName
    } else {
        Write-Log "No executable found in $outputPath"
    }

    # SECTION 5: GIT STATUS (Optional)
    if (Test-Path ".git") {
        Write-Section "Git status"
        try {
            $gitStatus = git status 2>&1
            foreach ($line in $gitStatus) {
                Write-Log $line
            }
        } catch {
            Write-ErrorLog "Failed to run 'git status': $_"
        }
    }

    # SECTION 6: DONE
    Write-Section "Build script completed"
} catch {
    Write-ErrorLog "Unexpected error occurred: $_"
    exit 1
}

pause
