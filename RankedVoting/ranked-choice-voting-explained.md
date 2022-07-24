
# Ranked choice voting

In Ranked Choice Voting (or Instanty Runoff Election) you rank candidates in order of preference. If your first choice gets more than 50% it wins and it is over, but if there is not enough people who voted the same as you did for your first preference, then your ballot is not wasted. Your second candidate will be promoted and your ballot still affects the outcome of the race. This systems makes every voter have maximum influence on the outcome of voting/elections.

It can be applied to any type of voting (including politics) but lets see an easy example:

You have 6 kids and they can't decide between a Dog, Cat, Mouse, and Bunny.

Kid|Pet
---|---
Kid 1|Dog
Kid 2|Dog
Kid 3|Cat
Kid 4|Cat
Kid 5|Mouse
Kid 6|Bunny

In ranked-choice everyone gets an additional choice

Kid|1st choice|2nd choice
---|---|---
Kid 1|Dog|Cat
Kid 2|Dog|Mouse
Kid 3|Cat|Bunny
Kid 4|Cat|Bunny
Kid 5|Mouse|Bunny
Kid 6|Bunny|Mouse

## Round 1 
In the first round we look only at the first choice

Kid|1st choice|2nd choice
---|---|---
Kid 1|**Dog**|Cat
Kid 2|**Dog**|Mouse
Kid 3|**Cat**|Bunny
Kid 4|**Cat**|Bunny
Kid 5|**Mouse**|Bunny
Kid 6|**Bunny**|Mouse

**Dog** has 33.3% with 2 votes

**Cat** has 33.3% with 2 votes

**Mouse** has 16.7% with 1 votes

**Bunn** has 16.7% with 1 votes

Since there is **nobody with 50% of votes**, we have to **remove one with lowest number of votes**. 

But we have a tie here - **Mouse** and **Bunny** each have 1 votes.

so we count how many kids wanted each of them:

**Mouse** has 3 votes total

**Bunny** has 4 votes total

So **Mouse** lost this round and we remove it from from everyones ballot and promote their next choices.

------------------

## Round 2

Kid|1st choice|2nd choice
---|---|---
Kid 1|**Dog**|Cat
Kid 2|**Dog**|~~Mouse~~
Kid 3|**Cat**|Bunny
Kid 4|**Cat**|Bunny
Kid 5|~~Mouse~~|**Bunny**
Kid 6|**Bunny**|~~Mouse~~

**Dog** has 33.3% with 2 votes

**Cat** has 33.3% with 2 votes

**Bunny** has 33.3% with 2 votes

Still nobody with more than 50% of votes. 

Tie for removal! **Dog**, **Cat**, and **Bunny** have 2 votes
so we count how many wanted each

**Dog** has 2 votes total

**Cat** has 3 votes total

**Bunny** has 4 votes total

So **Dog** lost this round with only 2 votes (sorry dog lovers)

------------------

## Round 3

Kid|1st choice|2nd choice
---|---|---
Kid 1|~~Dog~~|**Cat**
Kid 2|~~Dog~~|~~Mouse~~
Kid 3|**Cat**|Bunny
Kid 4|**Cat**|Bunny
Kid 5|~~Mouse~~|**Bunny**
Kid 6|**Bunny**|~~Mouse~~

Now **Cat** has 60% with 3 votes

**Bunny** has 40% with 2 votes

**Cat won**

This would be hard to count by hand if we had hundreds of votes so this program can help finish counting all rounds in less than a second.
____________

# Example 2

Now let's see how a **3rd choice can make a difference**.

We have the same 1st and a second choice, but kids can add a 3rd choice

Kid|1st choice|2nd choice|3rd choice
---|---|---|---
Kid 1|**Dog**|Cat|Mouse
Kid 2|**Dog**|Mouse|Bunny
Kid 3|**Cat**|Bunny|Dog
Kid 4|**Cat**|Bunny|Dog
Kid 5|**Mouse**|Bunny|Dog
Kid 6|**Bunny**|Mouse|Dog


## Round 1

**Dog** has 33.3% with 2 votes

**Cat** has 33.3% with 2 votes

**Mouse** has 16.7% with 1 votes

**Bunny** has 16.7% with 1 votes

Tie for removal! Candidates **Mouse** and **Bunny** have 1 votes

**Mouse** has 4 votes total

**Bunny** has 5 votes total

**Mouse** lost this round

------------------

## Round 2

Kid|1st choice|2nd choice|3rd choice
---|---|---|---
Kid 1|**Dog**|Cat|~~Mouse~~
Kid 2|**Dog**|~~Mouse~~|Bunny
Kid 3|**Cat**|Bunny|Dog
Kid 4|**Cat**|Bunny|Dog
Kid 5|~~Mouse~~|**Bunny**|Dog
Kid 6|**Bunny**|~~Mouse~~|Dog

**Dog** has 33.3% with 2 votes

**Cat** has 33.3% with 2 votes

**Bunny** has 33.3% with 2 votes

Tie! All 3 now have 2 votes, so we count totals

**Dog** has 6 votes total

**Cat** has 3 votes total

**Bunny** has 5 votes total

**Cat** lost this round

------------------

## Round 3

Kid|1st choice|2nd choice|3rd choice
---|---|---|---
Kid 1|**Dog**|~~Cat~~|~~Mouse~~
Kid 2|**Dog**|~~Mouse~~|Bunny
Kid 3|~~Cat~~|**Bunny**|Dog
Kid 4|~~Cat~~|**Bunny**|Dog
Kid 5|~~Mouse~~|**Bunny**|Dog
Kid 6|**Bunny**|~~Mouse~~|Dog

**Bunny** has 66.7% with 4 votes

**Dog** has 33.3% with 2 votes

This time **Bunny won** with 4 votes out of 6 ballots

------------------

# Make your votes count

- Don't select only one candidate (unless you really dislike other candidates)

- Don't select the same one 3 times - it won't make it 3 times stronger and instead it will just waste your other votes.

Kid|1st choice|2nd choice|3rd choice
---|---|---|---
Kid 1|**Mouse**|~~Mouse~~|~~Mouse~~

When there is a tie the algorithm just counts if someone voted for a candidate in any of their choices, so having the same candidate for all 3 will just leave nobody to be promoted if that candidate is removed.

