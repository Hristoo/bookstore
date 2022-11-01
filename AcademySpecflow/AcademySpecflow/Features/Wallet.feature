Feature: Wallet

Scenario: I need a drink
Given I withdraw 20 leva from the ATM
	When I buy a water for 2 leva from the store
	Then I should have 18 leva in my pocket


