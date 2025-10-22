# AutoMapper Integration Guide

This guide explains how AutoMapper has been integrated into the Library Management System project and how to use it effectively.

## What is AutoMapper?

AutoMapper is a library that provides object-to-object mapping. It eliminates the need to manually map properties between different objects, making your code cleaner and more maintainable.

## Project Structure

The AutoMapper integration includes:

1. **MappingProfile.cs** - Contains all mapping configurations
2. **DTOs** - Data Transfer Objects for API communication
3. **Entities** - Domain models representing database tables
4. **Services** - Business logic with AutoMapper integration
5. **Controllers** - API endpoints using AutoMapper

## Configuration

### 1. Package References

The following packages have been added:

```xml
<!-- In LibraryManagmentSystem.Services.csproj -->
<PackageReference Include="AutoMapper" Version="13.0.1" />

<!-- In LibraryManagmentSystem.Api.csproj -->
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
```

### 2. Dependency Injection Registration

In `Program.cs`:

```csharp
// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
```

### 3. Mapping Configuration

All mappings are configured in `MappingProfile.cs`:

```csharp
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Author mappings
        CreateMap<Author, AuthorResponseDto>()
            .ForMember(dest => dest.BooksCount, opt => opt.MapFrom(src => src.Books != null ? src.Books.Count : 0));

        CreateMap<AuthorCreateDto, Author>();
        CreateMap<AuthorUpdateDto, Author>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Book mappings
        CreateMap<Book, BookResponseDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author != null ? $"{src.Author.FirstName} {src.Author.LastName}".Trim() : ""))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : ""))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Reviews != null && src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0.0))
            .ForMember(dest => dest.TotalReviews, opt => opt.MapFrom(src => src.Reviews != null ? src.Reviews.Count : 0));

        // ... more mappings
    }
}
```

## Usage Examples

### 1. In Services

```csharp
public class BookService
{
    private readonly IMapper _mapper;
    
    public BookService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<BookResponseDto> CreateBookAsync(BookCreateDto bookCreateDto)
    {
        // Map DTO to Entity
        var book = _mapper.Map<Book>(bookCreateDto);
        
        // Save to database
        await _bookRepository.AddAsync(book);
        await _unitOfWork.CompleteAsync();
        
        // Map Entity to Response DTO
        return _mapper.Map<BookResponseDto>(book);
    }

    public async Task<BookResponseDto> GetBookByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        return _mapper.Map<BookResponseDto>(book);
    }
}
```

### 2. In Controllers

```csharp
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMapper _mapper;

    [HttpPost]
    public async Task<ActionResult<BookResponseDto>> CreateBook(BookCreateDto bookCreateDto)
    {
        // Map DTO to Entity
        var book = _mapper.Map<Book>(bookCreateDto);
        
        // Save and return mapped response
        var bookResponseDto = _mapper.Map<BookResponseDto>(book);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookResponseDto);
    }
}
```

## Mapping Types

### 1. Basic Mappings
- **Entity to DTO**: `CreateMap<Book, BookResponseDto>()`
- **DTO to Entity**: `CreateMap<BookCreateDto, Book>()`
- **Update Mappings**: `CreateMap<BookUpdateDto, Book>()`

### 2. Custom Mappings
```csharp
CreateMap<Book, BookResponseDto>()
    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => 
        src.Author != null ? $"{src.Author.FirstName} {src.Author.LastName}".Trim() : ""))
    .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => 
        src.Reviews != null && src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0.0));
```

### 3. Conditional Mappings
```csharp
CreateMap<BookUpdateDto, Book>()
    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
```

## Available Mappings

### Author Mappings
- `Author` ↔ `AuthorResponseDto`
- `AuthorCreateDto` → `Author`
- `AuthorUpdateDto` → `Author`
- `Author` → `AuthorWithBooksDto`

### Book Mappings
- `Book` ↔ `BookResponseDto`
- `BookCreateDto` → `Book`
- `BookUpdateDto` → `Book`
- `Book` → `BookWithDetailsDto`

### User Mappings
- `User` ↔ `UserResponseDto`
- `UserCreateDto` → `User`
- `UserUpdateDto` → `User`

### Category Mappings
- `Category` ↔ `CategoryResponseDto`
- `CategoryCreateDto` → `Category`
- `CategoryUpdateDto` → `Category`
- `Category` → `CategoryWithBooksDto`

### Publisher Mappings
- `Publisher` ↔ `PublisherResponseDto`
- `PublisherCreateDto` → `Publisher`
- `PublisherUpdateDto` → `Publisher`
- `Publisher` → `PublisherWithBooksDto`

### Loan Mappings
- `Loan` ↔ `LoanResponseDto`
- `LoanCreateDto` → `Loan`
- `LoanUpdateDto` → `Loan`
- `Loan` → `LoanWithDetailsDto`

### Review Mappings
- `Review` ↔ `ReviewResponseDto`
- `ReviewCreateDto` → `Review`
- `ReviewUpdateDto` → `Review`
- `Review` → `ReviewWithDetailsDto`

### Fine Mappings
- `Fine` ↔ `FineResponseDto`
- `FineCreateDto` → `Fine`
- `FineUpdateDto` → `Fine`
- `Fine` → `FineWithDetailsDto`

## Best Practices

1. **Use Dependency Injection**: Always inject `IMapper` through constructor injection
2. **Keep Mappings Simple**: Avoid complex logic in mapping configurations
3. **Use Conditional Mapping**: For update operations, use conditional mapping to avoid overwriting with null values
4. **Test Mappings**: Write unit tests for your mapping configurations
5. **Performance**: AutoMapper is optimized for performance, but avoid mapping large collections unnecessarily

## Benefits

1. **Reduced Boilerplate**: No need to manually map properties
2. **Type Safety**: Compile-time checking for mapping configurations
3. **Maintainability**: Centralized mapping configuration
4. **Performance**: Optimized object-to-object mapping
5. **Flexibility**: Support for custom mapping logic and conditions

## Troubleshooting

### Common Issues

1. **Missing Navigation Properties**: Ensure all navigation properties are properly configured in entities
2. **Circular References**: Be careful with bidirectional navigation properties
3. **Null Reference Exceptions**: Use null checks in custom mapping expressions
4. **Performance Issues**: Avoid mapping large collections in tight loops

### Debugging

Use AutoMapper's built-in debugging features:

```csharp
// Enable mapping validation
Mapper.AssertConfigurationIsValid();
```

This integration provides a solid foundation for object mapping in your Library Management System, making your code more maintainable and reducing the chance of mapping errors.
