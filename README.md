SassSharp - Simple SASS Compiler in C#
=====================================

An AST based Sass compiler. I think ASTs are fascinating, and want to try my
hand at using them.

This is a work in progress, but hopefully I can get it to support the majority
of simple SASS operations.

## Usage

Don't,

At least not in anything that matters. It's pretty alpha

For screwing around? Sure: here's the simple use case:

```csharp
var sass = @"
  p {
    font-size:12px;
    color: red;

    a {
      font-weight : bold;
    }
  }";

var css = new SassCompiler().Compile(sass);

Assert.That(css, Is.EqualTo("p{font-size:12px;color:red;}p a{font-weight:bold;}"));
```

## Contributing

Happy to receive code review, input, or patches. Just do the usual fork, clone,
pull-request dance.

## Roadmap
* ~~Nested selectors~~
* Include files
* Variables
* Mixins
* Functions

## License

MIT
