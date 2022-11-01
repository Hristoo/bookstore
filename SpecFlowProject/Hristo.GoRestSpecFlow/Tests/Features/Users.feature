@Authenticate
Feature: Users

Scenario: Get all users
	Given I wat to prepare a request
	When I get all users from the users endpoint
	Then The response staus code should be OK
	And the response should contain a list of users

Scenario Outline: Create a new user
	Given I have already existing following user data
		| Name    | Email           | Gender | Status |
		| Hristo4 | test4@gmail.com | male   | active |
	When I send a wrong request to the users endpoint
	Then The response staus should be <statusCode>
	And The user should be created <statusCode>

Examples:
	| statusCode          |
	| Created             |
	| UnprocessableEntity |
	| NotFound            |

Scenario Outline: Update an existing user
	Given I have following user
		| Name    | Email           | Gender | Status |
		| Hristo5 | test5@gmail.com | male   | active |
	When I update a request to the users endpoint
	Then The response staus code should be OK
	And The user should be created <statusCode>