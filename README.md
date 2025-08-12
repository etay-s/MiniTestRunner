# MiniTestRunner

A minimal C# unit testing framework using reflection and custom attributes.\
Designed to showcase basic reflection and testing concepts.

## Features

- Mark test classes and methods with custom attributes.
- Supports setup (`[TestInit]`) and teardown (`[TestFinal]`) methods.
- Runs tests in order: setup, tests, teardown.
- Console output and error reporting.


## How It Works

- The test runner loads assemblies and finds classes marked with `[TestClass]`.
- It categorizes methods by [`TestInit]`, `[TestMethod]`, and `[TestFinal]`.
- Methods are invoked in order: setup, tests, teardown.
- Any exceptions are caught and printed to the console.
