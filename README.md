![Image](https://github.com/Brutich/RegEx/blob/master/doc/regexlogo-1.jpg?branch=master)


# Regular expressions in [Dynamo](https://github.com/DynamoDS/Dynamo) #

This Dynamo package provides access to the Regular expressions library.

Regular expressions provide a powerful, flexible, and efficient method for processing text. The extensive pattern-matching notation of regular expressions enables you to quickly parse large amounts of text to find specific character patterns; to validate text to ensure that it matches a predefined pattern (such as an email address); to extract, edit, replace, or delete text substrings; and to add the extracted strings to a collection in order to generate a report. For many applications that deal with strings or that parse large blocks of text, regular expressions are an indispensable tool.
[Regular Expression Language - Quick Reference](https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference?view=netframework-4.8)

## Regular Expression Examples ##
### Example 1: Replacing Substrings ###
Assume that a mailing list contains names that sometimes include a title (Mr., Mrs., Miss, or Ms.) along with a first and last name. If you do not want to include the titles when you generate envelope labels from the list, you can use a regular expression to remove the titles, as the following example illustrates.
![Image](https://github.com/Brutich/RegEx/blob/master/doc/regex-examples/examples-001.png)
The regular expression pattern ```(Mr\.? |Mrs\.? |Miss |Ms\.? )``` matches any occurrence of "Mr ", "Mr. ", "Mrs ", "Mrs. ", "Miss ", "Ms or "Ms. ". Regex.Replace node replaces the matched string with empty string; in other words, it removes it from the original string.
