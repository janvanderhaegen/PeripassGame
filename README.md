# Peripass Word Finder game

This is a small developer test submission.

The solution has a small console app that demonstrates the original issue and some tests (I honestly really love test-driven development), both of which were initially stubbed out using AI (I use Cursor a *lot*). 

The domain project was handcoded (wow, oldskool) and has only three classes: `WordIndex` that prepares the large list of words, `WordCombinationFinder` which is the pattern matching algorithm and `FindOptions` for being a tiny bit mindful that requirements might change and we'll prefer backwards binary compatiliby when we do.


In my normal coding I also tend to keep my code fairly simple and readable, and steer away from introducing design patterns without a cause or introducing abstractions or interfaces in preparation for an unknown change in requirements in the future.   

After all, the best code is the code you didn't write (YAGNI). We can cross those bridges when we get there, for example by extracting an interface from a class at that point and writing a second implementation, or encapsulating more pattern validity logic into the `FindOptions`.

 
## If I had more time
-  I don't feel comfortable submitting anything that would not have input validation, but it takes up too much space in my code blocks and makes the code less readable at a glance. I have some common patterns to reduce this to oneliners in small projects, or use FluentValidation, or both.
-  Result pattern is more performant than throwing exceptions if we'd expect a large % of invalid input and performance really matters.
-  Find a better name than `WordIndex`... `WordRepository`? `WordCombinationData`? `ConcatenationStation`?



## Initial challenge: 6 letter words

There's a file in the root of the repository, input.txt, that contains words of varying lengths (1 to 6 characters).

My objective is to show all combinations of those words that:

- Together form a word of 6 characters.
- That combination must also be present in input.txt.
 

### Example

When the program is run with this input:
```
foobar
fo
o
bar
```

Then the program should ouput:
```
fo+o+bar=foobar
```
