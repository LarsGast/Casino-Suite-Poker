# CasinoSuite.Poker

[![Nuget](https://img.shields.io/nuget/v/CasinoSuite.Poker?color=green)](https://www.nuget.org/packages/CasinoSuite.Poker)


## Table of Contents

- [About](#about)
- [Features](#features)
- [Installation](#installation)
- [Dependencies](#dependencies)
- [Usage](#usage)
- [Contributing and Feedback](#contributing-and-feedback)

## About

CasinoSuite.Poker is a comprehensive library for working with poker hands and cards in C#.
This library includes classes for representing poker hands, cards, and offers utility methods for evaluating and comparing poker hands. 
It's a valuable tool for developers building poker-related applications, games, or simulations.

## Features

This package includes the PokerHand.cs class.
Using this class, you can
* Determine the best hand possible given a collection of cards with PokerHand.getBestHand().
* Determine if a certain hand wins against another hand with pokerHand.winsAgainst().
* Determine the best hand from a list of hands iwht PokerHand.getWinningHand().

These tools should give you everything you need to create your own poker implementation, whether that's Texas Hold 'em, Omaha, or any of the beat the house variations.

## Installation

To use this package in your project, you can install it via NuGet Package Manager:

```shell
nuget install CasinoSuite.Poker
```

### Dependencies

Before using the CasinoSuite.Poker library, you should ensure that you have the following dependencies installed:

[![Nuget](https://img.shields.io/nuget/v/DeckOfPlayingCards?color=green)](https://www.nuget.org/packages/DeckOfPlayingCards) 

**DeckOfPlayingCards**: This library provides the `Deck` and `Card` classes for managing decks of cards and individual cards. You can install it via NuGet using the following command:

```shell
nuget install DeckOfPlayingCards
```

Without this library you will not be able to create and evaluate PokerHands.


## Usage

The code snippet below will demonstrate how to get a deck, draw some cards, create two hands, and determine which hand wins.

```cs
using PokerLibrary;
using DeckOfCardsLibrary;

// Get a new deck and shuffle it.
var deck = Deck.get();
deck.shuffle();

// Deal two cards to each player.
var cardsPlayerOne = new List<Card>();
cardsPlayerOne.Add(deck.draw());
cardsPlayerOne.Add(deck.draw());

var cardsPlayerTwo = new List<Card>();
cardsPlayerTwo.Add(deck.draw());
cardsPlayerTwo.Add(deck.draw());

// Deal the cards on the table;
var tableCards = new List<Card>();
tableCards.Add(deck.draw());
tableCards.Add(deck.draw());
tableCards.Add(deck.draw());
tableCards.Add(deck.draw());
tableCards.Add(deck.draw());

// Determine the strength of each hand.
var handPlayerOne = PokerHand.getBestHand(cardsPlayerOne.Union(tableCards));
var handPlayerTwo = PokerHand.getBestHand(cardsPlayerTwo.Union(tableCards));

// Get the winner.
var playerOneWins = handPlayerOne.winsAgainst(handPlayerTwo);

// Display the winner.
if (playerOneWins == true) {
	Console.WriteLine("Player 1 wins!");
}
else if (playerOneWins == false) {
	Console.WriteLine("Player 2 wins!");
}
else {
	Console.WriteLine("It's a draw!");
}
```

The GitHub repo also includes a console app which you can play around with.

## Contributing and Feedback

If you have questions, concerns, or would like to contribute to this project, there are several ways to get involved:

- **Open an Issue**: If you encounter a bug, have a feature request, or want to discuss improvements, please [open an issue](https://github.com/LarsGast/Casino-Suite-Poker/issues).

- **Start a Discussion**: You can also initiate a discussion in the [Discussions](https://github.com/LarsGast/Casino-Suite-Poker/discussions) section of this repository to share ideas or seek help.

- **Fork and Contribute**: If you'd like to make changes or enhancements to the project, fork the repository and submit a pull request with your proposed changes.

Your contributions and feedback are highly valued and essential to the improvement of this project. Thank you for your support!