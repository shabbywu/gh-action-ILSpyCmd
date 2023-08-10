using System;
using System.Diagnostics;
using System.CommandLine;

namespace scl;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var filesOption = new Option<string>(
            name: "--files",
            description: "Newline-delimited assemblies paths that is being decompiled.");

        var outpitDirOption = new Option<string>(
            name: "--output-dir",
            getDefaultValue: () => "",
            description: "The output directory, if omitted decompiler output is written to standard out.");

        var projectOption = new Option<bool>(
            name: "--project",
            description: "Decompile assembly as compilable project. This requires the output directory option.");
        
        var nestedDirectoriesOption = new Option<bool>(
            name: "--nested-directories",
            description: "Use nested directories for namespaces.");
    

        var rootCommand = new RootCommand("Sample app for System.CommandLine");
        rootCommand.AddOption(filesOption);
        rootCommand.AddOption(outpitDirOption);
        rootCommand.AddOption(projectOption);
        rootCommand.AddOption(nestedDirectoriesOption);

        rootCommand.SetHandler((files, outputDir, project, nestedDirectories) => 
            {
                List<string> parsedArgs = new List<string>();
                if (outputDir != "") {
                    parsedArgs.Add("-o");
                    parsedArgs.Add(outputDir);
                    if (project) {
                        parsedArgs.Add("--project");
                    }
                    if (nestedDirectories) {
                        parsedArgs.Add("--nested-directories");
                    }
                }
                foreach(string file in files.Split("\n")) {
                    parsedArgs.Add(file);
                }
                Console.WriteLine($"ilspycmd {string.Join(" ", parsedArgs)}");
                run(string.Join(" ", parsedArgs));
            },
            filesOption, outpitDirOption, projectOption, nestedDirectoriesOption);

        return await rootCommand.InvokeAsync(args);
    }

    static void run(string args) {
        // 创建一个 ProcessStartInfo 对象来设置要启动的进程信息
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "/app/.dotnet/tools/ilspycmd", 
            Arguments = args,
            UseShellExecute = true // 是否使用操作系统外壳来启动进程
        };

        // 创建一个 Process 对象并启动进程
        using (Process process = Process.Start(psi))
        {
            // 等待进程结束
            process.WaitForExit();

            // 获取进程的退出代码
            int exitCode = process.ExitCode;
            Console.WriteLine($"ilspycmd exit code: {exitCode}");
        }
    }
}