
# Api Documentation for the library

Generated from the triple-slash `\\\` comments found in the source code.

DoxFx appears to have a shortcoming when documenting C# 9/0 code; none of the `record` types are automatically documented, even when they have triple-slash `\\\` comments, as of 2021-05-29

Docfx,json can be configured to support multiple frameworks, and and when so configured, metadata generation works for TargetFramework of net5.0, netstandard2.1, and net47. It generates three sets of metadata, each in its own subdirectory below `obj/ApiDocumentation`. The Microsoft documentation has a `Version` dropdown, that allows for selection between the appropriate TargetFramework. My guess is that Docfx's `group` and/or `merge` feature would be the means to accomplish this, but the DocFx documentation is inadequate / unclear.

Until Multiple Versions / TargetFrameworks can be solved, the documentation will be built against just the net5.0 targetFramework
