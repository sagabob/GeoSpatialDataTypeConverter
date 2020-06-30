Feature: GeoConvertingService
	In order to use the page
	As a normal user
	I want to access the page's components and functions


Scenario: Show the correct page title
	Given The page url & the browser	
	When I access the page
	Then The result should be 'Geospatial Data Converter' on the screen

Scenario: Access some options of the input data type dropdownlist
	Given The page url & the browser	
	When I access the page
	When I select input dropdown-list
	Then Select KML from the dropdown-list

Scenario: Upload a file
	Given The page url & the browser	
	When I access the page
	And I locate the upload input
	Then I upload a file

Scenario: Get converted output file
	Given The page url & the browser	
	When I access the page
	And I locate the upload input	
	Then I upload a file and click submit