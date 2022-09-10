

1. Thinking is broken

 I found that people (my colleagues and even here Writing Automated Acceptance Tests with Spec Flow ) do not try to write as of Business point, but still write on level of UI Code. This is dead end and can lead to frustration mentioned. Business friendly steps are much less brittle and much more maintainable, much more understandable by QA (and Dev newcomers).
 
If to write Gherkin as usual programming code. 

2. Scaling

No data

Data inline

Inline table
4. Table input is teachable. You should always write ONE not table test and then users will see analogy regarding table tests and go with it.
 
Next should be in the same file near each other:
 
Scenario: Power of 10 to 2
...
When power 10 to 2
Then get 100
 
Scenario: Power
...
When power <number> to <power>
Then get <powered>
 
Examples:
| number | power | powered
| 5          |    3     |  125

Gherikin does 
Keyword-driven_testing

Inline query

Inline or extenral: YAML

Steps XML input refactored to each step as generator


3. I found that tests should be organized hierarchically per feature files: simple first, optional, complex features, corner cases, erroneous, erroneous with retry. E.g.
a. Feature: User login // contains absolutely simple cases literal
b. Feature: User login with optional // scenarios here can combine several steps into one out of  a. Users should refer to a. to learn dictionary.
c. Feature: User login with error
d. Feature: User login, error, then right // steps are compressed out of a. b. c., only NEW unique steps are written literally.


