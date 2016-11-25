# Fabrikam-Food
Phase 2

 This is a project made for Fabrikam Food, a small boutique restaurant based in Auckland City.
 
 Main Features of this application include:
  1. Using Azure Services to implement a backend for my xamarin application
    -Requesting Data and inserting Data onto Azure Portal Easy Tables:
      a. View Menu list to an Easy Table
      b. Post reservation to an Easy Table
      
  2. Map Analytics
    - Using Google API, when user requests on the map page, it returns:
      a. User's Current Address
      b. Distance between Fabrikam Food Restaurant and user's current location.
      c. Travel Time between Fabrikam Food Restaurant and user's current location.
    To achieve such, it makes use of the hardware to get GEOCODE, using https://github.com/jamesmontemagno/GeolocatorPlugin.
    Google GEOCODE API to convert to address and query Google Map Directions API.
    
 3. Facebook Authentication
  - Using Facebook Authentication (Configured on azure)
  - NOTE: Facebook authentications are required to post reservation.
  
 4. Microsoft Bot Framework
  - Integration of Bot Framework onto Fabrikam Application using Web Chat interface.
 
