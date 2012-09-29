# Introduction

I'm going to focus on implementation of **calculation of total** rather than implementation of set of complete user stories such as **set up price on some product**, **filling typical order** from scratch using inputs from different devices (touch screen, bar-code scanner, scales etc.)

# Scope

## Order

So I assume that at some moment there is an **order** with **items**. Each **item** has indication of what **product** it represents, **quantity** of it and **price per unit**. _The important proposition is that price is dictated by product_. So as soon as product is indicated then its **price per unit** can be retrieved from an appropriate application subsystem/module (e.g. PriceList). Implementation of such price list subsystem is out of current scope.


### Products by piece

I assume that products which are sold _by piece_ are added to the order by human in following 2-steps way:

1. product is identified by bar-code reader

	_OR selected via touch screen_

	_OR somehow else_

2. quantity is specified manually

	_OR by appropriate device_

So same products may appear few times in the order with different integral quantities starting from 1.


### Products by weight

Products which are sold _by weight_ are added to the order in following 3-steps way:

1. **product** is identified by human

	_OR even by intelligent scale_
	_OR in another way_

2. product's **price per unit** is properly calculated or retrieved _(for example from aforementiond PriceList subsystem)_

3. **quantity** is specified by scales

	_OR by another appropriate device_
	_OR even manually_

###Remarks on price per unit

So if product price is stored somewhere as $1 per 100g and scales detects 1 pound of it then it might appear in the order as item with *price per unit* equal to $4.5359 and *quantity* equal to 1 **OR** with *price per unit* equal to $1 and *quantity* equal to 4.5359.

Another example - let's say eggs are sold by $2 for dozen and somebody buys 6 eggs. Then it can appear in order as item with *price per unit* $2 and *quantity* 0.5 **OR** with *price per unit* equal to $1.6666 (= $2 / 12) and *quantity* equal to 6.


## Discounts

A set of **discounts** can be applyed to the **order**.


### Sales

Let's call **sale** such kind of discounts on _particular products_ when the price of product is lowered by some _percent_ or _exact amount of money_.


### Bulks

Let's call **bulk discount** such kind of discounts on some _amount of items of particular product_ when if present in the order then the price of product is lowered by some _percent_ or _exact amount of money_.

Examples:

1. "Buy group of 4 items as for $1.99 (regular $2.49)"
2. "Buy group of 5 items as for 20% less"


### Bundles

Let's call **bundle** such kind of discounts on _particular group of products_ when if present in the order then calculated by special price. Aforementioned _group of products_ can consist of same product items or can include different products.

Let's consider few examples here:

1. "Buy one bottle of water and get one half price"
2. "Buy two bottles of water and get one free"
3. "Buy three bottles of water and get one cup for free"

Let's call the products from the starting part of such marketing proposals as **bundle base** and products listed at the end as **bundle addition**.


### Coupons

According to requirements will implement the functionality related to **coupons**. As soon as the _order total_ will exceed some **thresholds** appropriate amount will be taken off bill.

For example:

- $5 off when you spend $100 or more 
- $15 off when you spend $200 or more 


## Receipt

The problem of printing the final receipt for the order is pretty interesting and provides spacious opportunities for _understandability_ and _readability_ enhancements as well as possibilities to apply different design patterns. Although it is so attractive, at the moment it won't be implemented nevertheless.
