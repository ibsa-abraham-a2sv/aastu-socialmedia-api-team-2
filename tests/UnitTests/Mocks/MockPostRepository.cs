using Application.Contracts.Persistence;
using MediatR;
using Moq;
using Domain.Post;

namespace UnitTests.Mocks;

public static class MockPostRepository
{
    public static Mock<IPostRepository> GetPostRepository()
    {
        var users = new List<Tuple<string, Guid>>
        {
            new Tuple<string, Guid>("Nathnael", Guid.NewGuid()),
            new Tuple<string, Guid>("Yonas", Guid.NewGuid()),
            new Tuple<string, Guid>("Fuad", Guid.NewGuid()),
            new Tuple<string, Guid>("Nardos", Guid.NewGuid()),
            new Tuple<string, Guid>("Kidus", Guid.NewGuid())
        };
         var follows = new List<Tuple<Guid, Guid>>
        {
            new Tuple<Guid, Guid>(users[0].Item2, users[1].Item2),
            new Tuple<Guid, Guid>(users[1].Item2, users[2].Item2),
            new Tuple<Guid, Guid>(users[3].Item2,users[3].Item2 ),
            
        };

        var posts = new List<Tuple<string, Guid>>
        {
            new Tuple<string, Guid>("Post1", Guid.NewGuid()),
            new Tuple<string, Guid>("Post2", Guid.NewGuid()),
            new Tuple<string, Guid>("Post3", Guid.NewGuid()),
            new Tuple<string, Guid>("Post4", Guid.NewGuid()),
            new Tuple<string, Guid>("Post5", Guid.NewGuid())
        };
          var postList = new List<Domain.Post.Post>
        {
            new Domain.Post.Post()
            {
                UserId = users[0].Item2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Id =  posts[1].Item2
            },
            new Domain.Post.Post()
            {
                UserId = users[1].Item2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Id = posts[2].Item2
            },
            new Domain.Post.Post()
            {
                UserId = users[0].Item2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Id = posts[0].Item2
            },
        };

        // var likesList = new List<Domain.Likes.Likes>
        // {
        //     new Domain.Likes.Likes()
        //     {
        //         UserId = users[0].Item2,
        //         CreatedAt = DateTime.UtcNow,
        //         UpdatedAt = DateTime.UtcNow,
        //         LikesId = posts[1].Item2
        //     },
        //     new Domain.Likes.Likes()
        //     {
        //         UserId = users[1].Item2,
        //         CreatedAt = DateTime.UtcNow,
        //         UpdatedAt = DateTime.UtcNow,
        //         LikesId = posts[2].Item2
        //     },
        //     new Domain.Likes.Likes()
        //     {
        //         UserId = users[0].Item2,
        //         CreatedAt = DateTime.UtcNow,
        //         UpdatedAt = DateTime.UtcNow,
        //         LikesId = posts[0].Item2
        //     },
        // };

        var mockRepo = new Mock<IPostRepository>();

        // mockRepo.Setup(r => r.CreatePost(It.IsAny<Post>()).ReturnsAsync((Post post) =>
        // {
        //     postList.Add(post);
        //     return post;
        // });
        mockRepo.Setup(r => r.CreatePost(It.IsAny<Domain.Post.Post>())).ReturnsAsync((Domain.Post.Post post) =>
{
    post.Id = Guid.NewGuid(); 
    post.CreatedAt = DateTime.UtcNow;
    post.UpdatedAt = DateTime.UtcNow; 
    postList.Add(post); 

    return post.Id;
});
  mockRepo.Setup(r => r.UpdatePost(It.IsAny<Domain.Post.Post>())).ReturnsAsync((Domain.Post.Post post) =>
{
    var existingPost= postList.FirstOrDefault(p => p.Id == post.Id);
    Console.WriteLine(existingPost);
    existingPost.UpdatedAt = DateTime.UtcNow; 
    existingPost.Content = post.Content;

    return existingPost.Id;
});

        mockRepo.Setup(r => r.GetAll()).ReturnsAsync(postList);

        // mockRepo.Setup(r => r.RemoveLike(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync((Guid userId, Guid likesId) =>
        // {
        //     var response = likesList.FirstOrDefault(u => u.LikesId == likesId && u.UserId == userId);

        //     if (response != null)
        //     {
        //         likesList.Remove(response);
        //     }

        //     return new Unit();
        // });

        mockRepo.Setup(r => r.GetPost(It.IsAny<Guid>())).ReturnsAsync((Guid postId) =>
        {
            var response = postList.FirstOrDefault(u => u.Id == postId);
            return response;
        });
          mockRepo.Setup(r => r.GetPosts(It.IsAny<int>(),It.IsAny<int>())).ReturnsAsync((int pageIndex, int pageSize) =>
        {
         var newList =   postList.Skip((pageIndex - 1) * pageSize)
    .Take(pageSize)
    .ToList();
            return newList;
        });
         mockRepo.Setup(r => r.GetPostsByUserId(It.IsAny<Guid>(), It.IsAny<int>(),It.IsAny<int>())).ReturnsAsync((Guid userId, int pageIndex, int pageSize) =>
        {
         var newList = postList.Where(post => post.UserId == userId).Skip((pageIndex - 1) * pageSize)
    .Take(pageSize)
    .ToList();
            return newList;
        });
         mockRepo.Setup(r => r.GetFollowingPosts(It.IsAny<Guid>())).ReturnsAsync((Guid userId) =>
        {
            var following = follows.Where(follow => follow.Item1 == userId);
        var posts = new List<Domain.Post.Post>();

        foreach (var follow in following)
        {
            var userPosts = postList.Where(post => post.UserId == follow.Item2);
            posts.AddRange(userPosts);
        }
        return posts;
     
        });





        return mockRepo;

    }

}