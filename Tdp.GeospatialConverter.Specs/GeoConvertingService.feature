Feature: GeoConvertingService
	In order to use the page
	As a normal user
	I want to access the page's functions

@mytag
Scenario: Show the correct page title
	Given The page url	
	When I access the page
	Then the result should be "Geospatial Data Converter" on the screen
