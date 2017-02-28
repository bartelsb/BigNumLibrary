# README #

## Custom Implementation of a Big Number Class ##

### Summary ###
I started this project during one of my classes in college. My goal was to become more familiar with C#, specifically with overriding operators. I also used TDD during my initial implementation, although I got away from that approach after graduation. 

This project was interesting because it had a lot of mathematical complexity in addition to the coding challenges it presented. While the end result isn't very useful (there are far more efficient implementations of such classes in nearly every major language; in C#, that implementation is the [BigInteger](https://msdn.microsoft.com/en-us/library/system.numerics.biginteger(v=vs.110).aspx) class), I had a lot of fun working on this and also learned quite a bit. In the end, I was able to get everything working pretty well except for division. I gave up on trying to implement division well and settled on an extremely naive approach that takes ages to run. Otherwise, all of the operators appear to be implemented correctly!

### Using this class ###
As I mentioned above, this class is not intended to be used for a real project. If you are looking for a real implementation of a big number class, see Microsoft's implementation above.

That being said, if you'd like to just test out the code and see it in action, simply add a reference to this class in your own project. In my own testing, I added it as a reference to a calculator sample solution on the Visual Studio home page, and replaced each instance of an integer with my own class. It was as simple as that to begin using my code!