properties {
    $project_name     = "Tamarack"
    $base_directory   = resolve-path "..\."
    $build_directory  = "$base_directory\Release"
    $nunit_path       = "$base_directory\packages\NUnit.2.5.9.10348\Tools\nunit-console.exe"
    $nuget_path       = "$base_directory\packages\NuGet.CommandLine.1.4.20615.182\tools\"
    $solution_path    = "$base_directory\$project_name.sln"
    $nuspec_path      = "$base_directory\$project_name.nuspec"
    $release_files    =  @("$project_name.dll", "$project_name.pdb");  
}

task Publish -depends package {
    
    # Publish the nuget package
    # Todo
}

task Package -depends Default{
    
    # Get the version of the assembly, so no runtime patching is needed
    Write-Host "Getting version of assembly"
    $version = (Get-Command $build_directory\4.0\Tamarack.dll).FileVersionInfo.ProductVersion   
    Write-Host "Current is $version"
    
    # Create the nuget package
    Exec { & $nuget_path\nuget.exe pack $nuspec_path -Version "$version" -OutputDirectory "$build_directory" -Symbols }
}

task default -depends Build

task Clean -description "This task cleans up the build directory" {
       
    # Delete the build directory for a clean start    
    Remove-Item $build_directory -Force -Recurse -ErrorAction SilentlyContinue
}

task Build -depends Clean {

    # Build version for .NET 4.0
    Exec { Invoke-psake -framework '4.0' .\build.ps1 }
    
    "Finished 4.0 Build"
    
    # Build version for .NET 3.5
    Exec { Invoke-psake -framework '3.5' .\build.ps1 }
    
    "Finished 3.5 Build"
}