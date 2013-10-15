properties {
	$current_directory = Resolve-Path .
	$solution_directory = Join-Path $current_directory -ChildPath "source"
	$solution = Join-Path $solution_directory -ChildPath "MongoDB.Extensions.sln"
	$artifacts_directory = Join-Path $current_directory -ChildPath "artifacts"
	$lib_directory = Join-Path $artifacts_directory -ChildPath "lib"
	$content_directory = Join-Path $artifacts_directory -ChildPath "content"
	$tools_directory = Join-Path $artifacts_directory -ChildPath "tools"
	$nuget_exec = Join-Path $solution_directory -ChildPath ".nuget\nuget.exe"
	
	$config = 'Release'
	$framework = 'v4.5'	
}

task default -depends build

task clean_artifacts {
	$artifacts_exists = Test-Path $artifacts_directory
	If ($artifacts_exists -eq $True) {
		Remove-Item $artifacts_directory -Force -Recurse 
	}
	New-Item $artifacts_directory -itemType directory
	New-Item $lib_directory -itemType directory
	New-Item $content_directory -itemType directory
	New-Item $tools_directory -itemType directory
}

task clean_solution {
	Exec { msbuild $solution /t:Clean /p:Configuration=$config /p:TargetFrameworkVersion=$framework /v:quiet  } 
}

task build -depends clean_solution {
	Exec { msbuild $solution /t:Build /p:Configuration=$config /p:TargetFrameworkVersion=$framework /v:quiet /ds  } 
}

task prepare_artifacts -depends clean_artifacts, build {
	$libs = New-Object System.Collections.Generic.List[string]
	$libs.Add((Join-Path -Path $solution_directory -ChildPath "MongoDB.Extensions\bin\$config\MongoDB.Extensions.dll"))
	$libs.Add((Join-Path -Path $solution_directory -ChildPath "MongoDB.Extensions\bin\$config\MongoDB.Extensions.pdb"))
	
	$libs | Copy-Item -Destination $lib_directory
}

task create_package -depends prepare_artifacts {
	$package_name = Join-Path -Path $artifacts_directory -ChildPath "MongoDB.Extensions.nuspec"
	$package_template = Join-Path -Path $current_directory -ChildPath "template.nuspec"
	Copy-Item $package_template $package_name
	
	$assembly_info = Join-Path -Path $solution_directory -ChildPath "VersionInfo.cs"
	$assembly_pattern = "[0-9]+(\.([0-9]+|\*)){1,3}"
	$assembly_version_pattern = "^[^']*(AssemblyFileVersion[(""""].)([^""""]*)"
	$assembly_info_content = Get-Content $assembly_info
	$version_group = $assembly_info_content -replace " ", "" | Select-String -pattern $assembly_version_pattern | Select -first 1 | % { $_.Matches }              
    $assembly_version = $version_group.Groups[2].Value 
	
	[xml] $spec = Get-Content $package_name
    $spec.package.metadata.version = $assembly_version
	$spec.Save($package_name)
	
	& $nuget_exec pack $package_name -OutputDirectory $artifacts_directory 
}

task publish -depends create_package {
	$server = "https://www.nuget.org/api/v2/"
	$api_key = Get-Content (Join-Path -Path . -ChildPath "apikey")
	
	Write-Host $api_key
	
	Get-ChildItem $artifacts_directory -Recurse -Include "*.nupkg" | 
		ForEach-Object {
			#& $nuget_exec push -Source $server $_.fullname $api_key
			& $nuget_exec push $_.fullname $api_key
        }
}