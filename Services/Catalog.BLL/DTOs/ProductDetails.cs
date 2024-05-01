﻿namespace Catalog.BLL.DTOs;

public class ProductDetails
{
    public ProductDto Product { get; set; }
    public IEnumerable<GenreDto> Genres { get; set; }
    public IEnumerable<ActorDto> Actors { get; set; }
    public IEnumerable<DirectorDto> Directors { get; set; }
    public IEnumerable<ScreeningDto> Screenings { get; set; }
}