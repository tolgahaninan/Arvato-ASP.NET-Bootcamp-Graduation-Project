
# Arvato .NET Bootcamp Graduation Project - Movies API

#### An ASP.NET CORE API Project with Following Technologies

● Net 6
● Ef Core
● Web Api
● Postgresql
● Swagger
● Redis
● Background Job Worker

#### Description 

This is a Web API Project with 4 different kind of end points.  
Dataset which is used can be added with following link.    
[Dataset Scripts](https://drive.google.com/drive/folders/1lELrjMu_1s8bmn4vDUXqH3Zgg3zmw61O?usp=sharing).

## Genres

[![whpFCG.md.png](https://iili.io/whpFCG.md.png)](https://freeimage.host/i/whpFCG)


### Genres Usage

#### Brings all genres 
```http
  GET /api/Genre
```
#### Brings a genre which is specified by genreId
##### Not working
```http
  GET /api/Genre/{genreId}
```
#### Create a new Genre
```http
  POST /api/Genre
```
#### Updates an already existed genre
```http
  PUT /api/Genre/{genreId}
```
#### Deletes an already existed genre
```http
  DELETE /api/Genre/{genreId}
```

## Movies 

[![whyBVa.md.png](https://iili.io/whyBVa.md.png)](https://freeimage.host/i/whyBVa)

### Movies Usage

#### Brings a movie which is specified by movieId
```http
  GET /api/Movie/{movieId}
```
#### Brings a movie list which can be specified by title , rate and year of movie
```http
  GET /api/Movie/SearchMovie
```
#### Brings a movie list which is specified by genreId
```http
  GET /api/Movie/GetMovieListByGenres/{genreId}
```
#### Brings a movie list which is specified by rate Filter 
```http
  GET /api/Movie/GetMovieListByRates/{rateFilter}
```
#### Brings a movie list which is specified by releaseDate
```http
  GET /api/Movie/GetMovieListByReleaseDate/{releaseData}
```
#### Create a new movie
```http
  POST /api/Movie
```
#### Updates an already existed movie
```http
  PUT /api/Movie/{movieId}
```
#### Deletes an already existed movie
```http
  DELETE /api/Movie/{movieId}
```

## Trends 

[![whpnvS.md.png](https://iili.io/whpnvS.md.png)](https://freeimage.host/i/whpnvS)

#### Brings a movie list which are the highest rated movies
```http
  GET /api/Trend/ListTopRatedMovies
```
#### Brings a movie list which are the highest viewed movies
```http
  GET /api/Trend/ListTopViewedMovies
```
## Users 

[![whpzu9.md.png](https://iili.io/whpzu9.md.png)](https://freeimage.host/i/whpzu9)

#### To get JWT Access token for existed user
```http
  GET /api/user/SignIn
```





## Features

#### Authentication

A JWT token access mechanism is used to authenticate users.   
All endpoints below Genre, Movie and trends needs authentication.  
JWT Token is expiring after ten minutes and User have to get new token.

#### Background Job Worker

A Background Job worker is defined to save TotalViewCount of all the movies.  
It is getting ViewCount data of movies from PostegreSql database.  
After getting them addition operation of all View count data is made and TotalViewCount is
stored to Redis database.  

TotalViewCount can be accesesed from redis-cli

```redis-cli
get "TotalMovieView"
```

Background Job Worker works in every one minute and store TotalViewCount into Redis database

#### Exception Handling with Custom Middleware

A custom middleware for exception handling is created. Whenever system gets an exception middleware
gets actived and handles the exception according to the type of exception.

#### Movie Detail Call Information

To keep Movies view count MovieView table is created into Postgresql database.

After each call of ;
```http
  GET /api/Genre/{genreId}
```

If movie is alredy viewed, MovieView table clickCount property is incremented.  
If movie is viewed for the first time, A new record with "clickCount=1" is added to MovieView Table

#### Login Validation and Encryption

Since the Project doesn't contain register mechanism, The data stored to the user table 
which contains user's username and password is considered to pre-added. Also password column's data
considered to encrypted before pre-additon operation.  

Make Sure that your database user password value is hashed with SHA256 Algorithm  
You can hash your password before inserting into database via using  [SHA256 Hasher](https://xorbin.com/tools/sha256-hash-calculator).

