#!/bin/bash

exe="./TestRunner/bin/Debug/net6.0/TestRunner.exe"

dll="./DeveloperCodeAndTests/bin/Debug/net6.0/DeveloperCodeAndTests.dll"

$exe $dll

exec $SHELL
