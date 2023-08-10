<div align="center">
  ⛏️📦 
</div>
<h1 align="center">
  action gh-action-ILSpyCmd
</h1>

<p align="center">
   GitHub Action for decompiling C# Assembly files.
</p>

<br />

## 🤸 Usage


```yaml
name: Main

on: push

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout
        uses: actions/checkout@v3
      - name: Decompile
        uses: shabbywu/gh-action-ILSpyCmd@v1
        with:
            files: |
                Assembly-CSharp.dll
```

## 💅 Customizing

### inputs

The following are optional as `step.with` keys

| Name                       | Type    | Description                                                                                                                                                                                                                                                                                                                                                                                                                                     |
| -------------------------- | ------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `files`                     | String  | Newline-delimited assemblies paths that is being decompiled.                                                                                                                                                                                                                                                                                                                                                                                              |
| `output-dir`                | String  | The output directory, if omitted decompiler output is written to standard out.                                                                                                                                                                                                                                                                                                                                                                                 |
| `project`                    | Boolean | Dceompile assembly as compilable project. This requires the output directory option.                                                                                                                                                                                                                                                                                                                                                                                             |
| `nested-directories`               | Boolean | Use nested directories for namespaces.                                                                                                                                                                                                                         

💡 When not providing `output-dir`, the decompilded soure code will print to the stdout.

💡 When not providing `output-dir`, `project` and `nested-directories` will be ignored.
