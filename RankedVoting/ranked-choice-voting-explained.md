
#Ranked choice voting

It can be applied to any type of voting (including political) but lets see an easy example

You have 6 kids and they can't decide between a dog, cat, mouse and bunny

Kid|Pet
---|---
Kid 1|dog
Kid 2|dog
Kid 3|cat
Kid 4|cat
Kid 5|mouse
Kid 6|bunny

In ranked-choice everyone gets an additional choice

Kid|1st choice|2nd choice
---|---|---
Kid 1|dog|cat
Kid 2|dog|mouse
Kid 3|cat|bunny
Kid 4|cat|bunny
Kid 5|mouse|bunny
Kid 6|bunny|mouse

##Round 1 
looks only at the first choice

Kid|1st choice|2nd choice
---|---|---
Kid 1|**dog**|cat
Kid 2|**dog**|mouse
Kid 3|**cat**|bunny
Kid 4|**cat**|bunny
Kid 5|**mouse**|bunny
Kid 6|**bunny**|mouse

dog has 33.3% with 2 votes
cat has 33.3% with 2 votes
mouse has 16.7% with 1 votes
bunny has 16.7% with 1 votes

Tie for removal! Candidates mouse and bunny have 1 votes
so we count how many wanted each
Candidate mouse has 3 votes total
Candidate bunny has 4 votes total

mouse lost this round and we remove it from everywhere and move the look at the next choices
------------------

##Round 2

Kid|1st choice|2nd choice
---|---|---
Kid 1|**dog**|cat
Kid 2|**dog**|~~mouse~~
Kid 3|**cat**|bunny
Kid 4|**cat**|bunny
Kid 5|~~mouse~~|**bunny**
Kid 6|**bunny**|~~mouse~~

dog has 33.3% with 2 votes
cat has 33.3% with 2 votes
bunny has 33.3% with 2 votes

Tie for removal! Candidates dog, cat, and bunny have 2 votes
so we count how many wanted each
Candidate dog has 2 votes total
Candidate cat has 3 votes total
Candidate bunny has 4 votes total

dog lost this round with only 2 votes
------------------

##Round 3

Kid|1st choice|2nd choice
---|---|---
Kid 1|~~dog~~|**cat**
Kid 2|~~dog~~|**mouse**
Kid 3|**cat**|bunny
Kid 4|**cat**|bunny
Kid 5|~~mouse~~|**bunny**
Kid 6|**bunny**|~~mouse~~

Now cat has 60% with 3 votes
bunny has 40% with 2 votes

**Cat won**
____________

Example 2

Now 3rd choice can make a difference

Kid,1st choice,2nd choice,3rd choice
---|---|---|---
Kid 1,**dog**,cat,mouse
Kid 2,**dog**,mouse,bunny
Kid 3,**cat**,bunny,dog
Kid 4,**cat**,bunny,dog
Kid 5,**mouse**,bunny,dog
Kid 6,**bunny**,mouse,dog


##Round 1

dog has 33.3% with 2 votes
cat has 33.3% with 2 votes
mouse has 16.7% with 1 votes
bunny has 16.7% with 1 votes

Tie for removal! Candidates mouse, bunny have 1 votes
Candidate mouse has 4 votes total
Candidate bunny has 5 votes total

mouse lost this round
------------------

##Round 2

Kid,1st choice,2nd choice,3rd choice
---|---|---|---
Kid 1,**dog**,cat,~~mouse~~
Kid 2,**dog**,~~mouse~~,bunny
Kid 3,**cat**,bunny,dog
Kid 4,**cat**,bunny,dog
Kid 5,~~mouse~~,**bunny**,dog
Kid 6,**bunny**,~~mouse~~,dog

dog has 33.3% with 2 votes
cat has 33.3% with 2 votes
bunny has 33.3% with 2 votes

Tie for removal! Candidates dog, cat, bunny have 2 votes
Candidate dog has 6 votes total
Candidate cat has 3 votes total
Candidate bunny has 5 votes total

cat lost this round
------------------

##Round 3

Kid,1st choice,2nd choice,3rd choice
---|---|---|---
Kid 1,**dog**,~~cat~~,~~mouse~~
Kid 2,**dog**,~~mouse~~,bunny
Kid 3,~~cat~~,**bunny**,dog
Kid 4,~~cat~~,**bunny**,dog
Kid 5,~~mouse~~,**bunny**,dog
Kid 6,**bunny**,~~mouse~~,dog

bunny has 66.7% with 4 votes
dog has 33.3% with 2 votes

Candidate **bunny won** with 4 votes out of 6 ballots
66.67% selected bunny as one of their preferences

------------------

Some make mistakes, selecting the same one 3 times, but algorithm just checks if someone voted for candidate and will count just the first the next preference and when removing it will remove all 3 at the same time and just wastes 2 votes
Kid,1st choice,2nd choice,3rd choice
---|---|---|---
Kid 1,**mouse**,mouse,mouse