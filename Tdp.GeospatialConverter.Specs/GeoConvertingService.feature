Feature: GeoConvertingService
	In order to use the page
	As a normal user
	I want to access the page's components and functions


Scenario: Show the correct page title
	Given The page url & the browser	
	When I access the page
	Then the result should be 'Geospatial Data Converter' on the screen


Scenario: Access all options of the input data type dropdownlist
	Given The page url & the browser
	When I access the page
	And I select KML option
	Then The dropdown list should show KML value