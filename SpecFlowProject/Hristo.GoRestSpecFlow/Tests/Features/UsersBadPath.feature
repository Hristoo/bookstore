Feature: UsersBadPath

Scenario: Get all users
	Given I wat to prepare a bad request
	When I try get all users from the users endpoint
	Then The response staus should be NotFound
	
@Authenticate
Scenario: Create a new user
	Given I have already existing following user data
		| Name   | Email          | Gender | Status |
		| Hristo | test@gmail.com | male   | active |
	When I send a wrong request to the users endpoint
	Then The response staus should be UnprocessableEntity
