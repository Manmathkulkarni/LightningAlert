# DTN job interview assignment

## Details

I have used VS 2019 Edition for this assignment with .Net framework 4.7.2 
It contains two projects. 
1. Console application project
2. Unit testing project

Lightning and assets json file are added on project level for make it easy to reading. 

Prerequsite:
Newtoonsoft json(Latest stable version) is used for deserializing Json file into object.

## How to run the program
Compile the solution and run the program.
Program will run and read both json files and show outpu on the console. 
Application window stays open till user press Enter key.

## Answer to questions:
- What is the [time complexity] : Time complxity for this algorithm is O(n log n)

- If we put this code into production, but found it too slow, or it needed to scale to many more users or more frequent strikes, what are the first things you would think of to speed it up?
: If this program runs too slow, parsing json file can be improved. Logic to generate Quadkey can be improved.

  For scaling up to many more users, multiple instance of this service can be deployed to parse incoming json records. 
  Strikes that has occured already can be saved in common/global file that can be accesses by multiple instances.
  