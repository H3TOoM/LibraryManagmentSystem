using AutoMapper;
using LibraryManagmentSystem.Data.Entities;
using LibraryManagmentSystem.Services.DTOs;

namespace LibraryManagmentSystem.Services.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Author mappings
            CreateMap<Author, AuthorResponseDto>()
                .ForMember( dest => dest.BooksCount, opt => opt.MapFrom( src => src.Books != null ? src.Books.Count : 0 ) );

            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorUpdateDto, Author>()
                .ForAllMembers( opts => opts.Condition( ( src, dest, srcMember ) => srcMember != null ) );

            CreateMap<Author, AuthorWithBooksDto>()
                .ForMember( dest => dest.BooksCount, opt => opt.MapFrom( src => src.Books != null ? src.Books.Count : 0 ) )
                .ForMember( dest => dest.Books, opt => opt.MapFrom( src => src.Books ) );

            // Book mappings
            CreateMap<Book, BookResponseDto>()
                .ForMember( dest => dest.AuthorName, opt => opt.MapFrom( src => src.Author != null ? $"{src.Author.FirstName} {src.Author.LastName}".Trim() : "" ) )
                .ForMember( dest => dest.CategoryName, opt => opt.MapFrom( src => src.Category != null ? src.Category.Name : "" ) )
                .ForMember( dest => dest.AverageRating, opt => opt.MapFrom( src => src.Reviews != null && src.Reviews.Any() ? src.Reviews.Average( r => r.Rating ) : 0.0 ) )
                .ForMember( dest => dest.TotalReviews, opt => opt.MapFrom( src => src.Reviews != null ? src.Reviews.Count : 0 ) );

            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>()
                .ForAllMembers( opts => opts.Condition( ( src, dest, srcMember ) => srcMember != null ) );

            CreateMap<Book, BookWithDetailsDto>()
                .ForMember( dest => dest.AuthorName, opt => opt.MapFrom( src => src.Author != null ? $"{src.Author.FirstName} {src.Author.LastName}".Trim() : "" ) )
                .ForMember( dest => dest.CategoryName, opt => opt.MapFrom( src => src.Category != null ? src.Category.Name : "" ) )
                .ForMember( dest => dest.AverageRating, opt => opt.MapFrom( src => src.Reviews != null && src.Reviews.Any() ? src.Reviews.Average( r => r.Rating ) : 0.0 ) )
                .ForMember( dest => dest.TotalReviews, opt => opt.MapFrom( src => src.Reviews != null ? src.Reviews.Count : 0 ) )
                .ForMember( dest => dest.Author, opt => opt.MapFrom( src => src.Author ) )
                .ForMember( dest => dest.Category, opt => opt.MapFrom( src => src.Category ) )
                .ForMember( dest => dest.Reviews, opt => opt.MapFrom( src => src.Reviews ) );

            // User mappings
            CreateMap<User, UserResponseDto>();
            CreateMap<UserCreateDto, User>()
                .ForMember( dest => dest.PasswordHash, opt => opt.MapFrom( src => src.Password ) ); // In real app, hash the password
            CreateMap<UserUpdateDto, User>()
                .ForAllMembers( opts => opts.Condition( ( src, dest, srcMember ) => srcMember != null ) );

            // Category mappings
            CreateMap<Category, CategoryResponseDto>()
                .ForMember( dest => dest.BooksCount, opt => opt.MapFrom( src => src.Books != null ? src.Books.Count : 0 ) );

            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>()
                .ForAllMembers( opts => opts.Condition( ( src, dest, srcMember ) => srcMember != null ) );

            CreateMap<Category, CategoryWithBooksDto>()
                .ForMember( dest => dest.BooksCount, opt => opt.MapFrom( src => src.Books != null ? src.Books.Count : 0 ) )
                .ForMember( dest => dest.Books, opt => opt.MapFrom( src => src.Books ) );

            // Publisher mappings
            CreateMap<Publisher, PublisherResponseDto>()
                .ForMember( dest => dest.BooksCount, opt => opt.MapFrom( src => src.Books != null ? src.Books.Count : 0 ) );

            CreateMap<PublisherCreateDto, Publisher>();
            CreateMap<PublisherUpdateDto, Publisher>()
                .ForAllMembers( opts => opts.Condition( ( src, dest, srcMember ) => srcMember != null ) );

            CreateMap<Publisher, PublisherWithBooksDto>()
                .ForMember( dest => dest.BooksCount, opt => opt.MapFrom( src => src.Books != null ? src.Books.Count : 0 ) )
                .ForMember( dest => dest.Books, opt => opt.MapFrom( src => src.Books ) );

            // Loan mappings
            CreateMap<Loan, LoanResponseDto>()
                .ForMember( dest => dest.UserName, opt => opt.MapFrom( src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}".Trim() : "" ) )
                .ForMember( dest => dest.BookTitle, opt => opt.MapFrom( src => src.Book != null ? src.Book.Title : "" ) )
                .ForMember( dest => dest.IsOverdue, opt => opt.MapFrom( src => src.ReturnDate == null && DateTime.Now > src.DueDate ) )
                .ForMember( dest => dest.DaysOverdue, opt => opt.MapFrom( src => src.ReturnDate == null && DateTime.Now > src.DueDate ? (DateTime.Now - src.DueDate).Days : 0 ) );

            CreateMap<LoanCreateDto, Loan>();
            CreateMap<LoanUpdateDto, Loan>()
                .ForAllMembers( opts => opts.Condition( ( src, dest, srcMember ) => srcMember != null ) );

            CreateMap<Loan, LoanWithDetailsDto>()
                .ForMember( dest => dest.UserName, opt => opt.MapFrom( src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}".Trim() : "" ) )
                .ForMember( dest => dest.BookTitle, opt => opt.MapFrom( src => src.Book != null ? src.Book.Title : "" ) )
                .ForMember( dest => dest.IsOverdue, opt => opt.MapFrom( src => src.ReturnDate == null && DateTime.Now > src.DueDate ) )
                .ForMember( dest => dest.DaysOverdue, opt => opt.MapFrom( src => src.ReturnDate == null && DateTime.Now > src.DueDate ? (DateTime.Now - src.DueDate).Days : 0 ) )
                .ForMember( dest => dest.User, opt => opt.MapFrom( src => src.User ) )
                .ForMember( dest => dest.Book, opt => opt.MapFrom( src => src.Book ) )
                .ForMember( dest => dest.Fines, opt => opt.MapFrom( src => src.Fines ) );

            // Review mappings
            CreateMap<Review, ReviewResponseDto>()
                .ForMember( dest => dest.BookTitle, opt => opt.MapFrom( src => src.Book != null ? src.Book.Title : "" ) )
                .ForMember( dest => dest.UserName, opt => opt.MapFrom( src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}".Trim() : "" ) );

            CreateMap<ReviewCreateDto, Review>();
            CreateMap<ReviewUpdateDto, Review>()
                .ForAllMembers( opts => opts.Condition( ( src, dest, srcMember ) => srcMember != null ) );

            CreateMap<Review, ReviewWithDetailsDto>()
                .ForMember( dest => dest.BookTitle, opt => opt.MapFrom( src => src.Book != null ? src.Book.Title : "" ) )
                .ForMember( dest => dest.UserName, opt => opt.MapFrom( src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}".Trim() : "" ) )
                .ForMember( dest => dest.User, opt => opt.MapFrom( src => src.User ) )
                .ForMember( dest => dest.Book, opt => opt.MapFrom( src => src.Book ) );

            // Fine mappings
            CreateMap<Fine, FineResponseDto>()
                .ForMember( dest => dest.UserName, opt => opt.MapFrom( src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}".Trim() : "" ) )
                .ForMember( dest => dest.BookTitle, opt => opt.MapFrom( src => src.Loan != null && src.Loan.Book != null ? src.Loan.Book.Title : "" ) )
                .ForMember( dest => dest.CreatedDate, opt => opt.MapFrom( src => DateTime.Now ) ) // Assuming CreatedDate should be set
                .ForMember( dest => dest.PaidDate, opt => opt.MapFrom( src => src.IsPaid ? DateTime.Now : (DateTime?)null ) );

            CreateMap<FineCreateDto, Fine>();
            CreateMap<FineUpdateDto, Fine>()
                .ForAllMembers( opts => opts.Condition( ( src, dest, srcMember ) => srcMember != null ) );

            CreateMap<Fine, FineWithDetailsDto>()
                .ForMember( dest => dest.UserName, opt => opt.MapFrom( src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}".Trim() : "" ) )
                .ForMember( dest => dest.BookTitle, opt => opt.MapFrom( src => src.Loan != null && src.Loan.Book != null ? src.Loan.Book.Title : "" ) )
                .ForMember( dest => dest.CreatedDate, opt => opt.MapFrom( src => DateTime.Now ) )
                .ForMember( dest => dest.PaidDate, opt => opt.MapFrom( src => src.IsPaid ? DateTime.Now : (DateTime?)null ) )
                .ForMember( dest => dest.User, opt => opt.MapFrom( src => src.User ) )
                .ForMember( dest => dest.Loan, opt => opt.MapFrom( src => src.Loan ) );
        }
    }
}
