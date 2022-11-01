Feature: HooksExample

@Authenticate
Scenario: Create a new user
	Given I have following user data
		| Name   | Email            | Gender | Status |
		| Hristo | test11@gmail.com | male   | active |
	When I send a request to the users endpoint
	Then The response staus code should be Created
	And The user should be created successfully

@Authenticate2
Scenario: Create a new user2
	Given I have following user data
		| Name   | Email            | Gender | Status |
		| Ico | test112@gmail.com | male   | active |
	When I send a request to the users endpoint
	Then The response staus code should be Created
	And The user should be created successfully