﻿using System;
using Moq;
using MyStudyProject.Data.Internet.Assemblers;
using MyStudyProject.Shared.Contracts.Enums;
using Tweetinvi.Models;
using Xunit;

namespace MyStudyProject.Tests.Unit.Mappers
{
    public class TwitterMessageResultMapperTest
    {
        [Fact]
        public void TestTwitterMessageSingleMap()
        {
            //Arrange
            var mapper = new TwitterMessageResultMapper();
            var tweetMock = new Mock<ITweet>();
            var hash = "hash";
            tweetMock.SetupGet(x => x.Url).Returns("url");
            tweetMock.SetupGet(x => x.Text).Returns("Text");
            tweetMock.SetupGet(x => x.CreatedBy.IdStr).Returns("UserId");
            tweetMock.SetupGet(x => x.IdStr).Returns("IdStr");
            tweetMock.SetupGet(x => x.TweetLocalCreationDate).Returns(DateTime.MaxValue);
            var tweet = tweetMock.Object;

            //Act
            var result = mapper.MapSingle(tweet, hash);

            //Assert
            Assert.Equal(tweet.Url, result.User.Url);
            Assert.Equal(tweet.Text, result.MessageText);
            Assert.Equal(hash, result.HashTag);
            Assert.Equal(tweet.IdStr, result.NetworkId);
            Assert.Equal(SocialMediaType.Twitter, result.MediaType);
            Assert.Equal(tweet.TweetLocalCreationDate, result.PostDate);
        }
    }
}