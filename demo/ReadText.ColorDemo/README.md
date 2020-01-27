
1. Use Custom Parser and set  `StyleType = StyleType.Color`
 Also, you can enable Ansi color in Linux, OS X. On windows 7 set EnableAnsiColor=false (default)

```cs
var parser = new Parser(with =>
  {
    with.StyleType = StyleType.Color;
    with.HelpWriter = Console.Out;
	//with.EnableAnsiColor = true; //Ansi color is supported: linux, OS x and windows 10
  });
```  
2. Call `parser.ParseArguments`

```cs
var result = parser.ParseArguments<HeadOptions, TailOptions>(args);
var texts = result
		.MapResult(
				(HeadOptions opts) => Tuple.Create(header(opts), reader(opts)),
				(TailOptions opts) => Tuple.Create(header(opts), reader(opts)),
				_ => MakeError());
```					
