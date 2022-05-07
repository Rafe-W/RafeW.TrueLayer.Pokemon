using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RafeW.TrueLayer.Pokemon.Engine.Entities.Translations;
using RafeW.TrueLayer.Pokemon.Engine.Entities.Utilities;
using RafeW.TrueLayer.Pokemon.Engine.Exceptions;
using RafeW.TrueLayer.Pokemon.Engine.Tests.TestHelpers.ServiceContainers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Tests.Test.Services.Api
{
    [TestClass]
    public class TranslationsServiceTests
    {

        //Translation API is quite restrictive with rate limiting so highly likely to encouter a rate limited response. Therefore a particular exception with appropriate messaging for user is expected
        [TestMethod]
        public async Task ToShakespearean_429()
        {
            //Arrange
            var container = new TranslationsServiceContainer(true);
            var result = new RequestResult<TranslationResult>();
            var text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
            result.SetFailure(new HttpRequestException("429 Too many requests", null, HttpStatusCode.TooManyRequests));

            container.RequestHandlerService.Setup(r => r.TrySendRequest<TranslationResult>(It.IsAny<string>(), HttpMethod.Post, null)).ReturnsAsync(result);
            container.RequestHandlerService.SetupGet(r => r.Settings).Returns(new Translations_ApiSettings());

            try
            {
                //Act
                var  _ = await container.TranslationsService.ToShakespearean(text);
            }
            catch(Exception ex)
            {
                //Assert
                if (ex is PokemonTranslationApiException pokeEx)
                {
                    Assert.AreEqual(PokemonTranslationApiException.TooManyRequestsFriendlyMessage, pokeEx.FriendlyMessage);
                    Assert.AreEqual(HttpStatusCode.TooManyRequests, pokeEx.HttpStatusCode);
                    Assert.AreEqual(result.Exception, pokeEx.InnerException);
                    return;
                }
                else
                {
                    Assert.Fail("Unexpected exception type");
                }
            }

            Assert.Fail("No exception thrown");
        }

        [TestMethod]
        public async Task ToShakespearean_OtherErrorResult()
        {
            //Arrange
            var container = new TranslationsServiceContainer(true);
            var result = new RequestResult<TranslationResult>();
            var text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
            result.SetFailure(new HttpRequestException("500 Server Error", null, HttpStatusCode.InternalServerError));

            container.RequestHandlerService.Setup(r => r.TrySendRequest<TranslationResult>(It.IsAny<string>(), HttpMethod.Post, null)).ReturnsAsync(result);
            container.RequestHandlerService.SetupGet(r => r.Settings).Returns(new Translations_ApiSettings());

            try
            {
                //Act
                var _ = await container.TranslationsService.ToShakespearean(text);
            }
            catch (Exception ex)
            {
                //Assert
                if (ex is PokemonTranslationApiException pokeEx)
                {
                    Assert.AreEqual(PokemonTranslationApiException.GenericFriendlyMessage, pokeEx.FriendlyMessage);
                    Assert.AreEqual(HttpStatusCode.InternalServerError, pokeEx.HttpStatusCode);
                    Assert.AreEqual(result.Exception, pokeEx.InnerException);
                    return;
                }
                else
                {
                    Assert.Fail("Unexpected exception type");
                }
            }

            Assert.Fail("No exception thrown");
        }

        [TestMethod]
        public async Task ToShakespearean_Success()
        {
            //Arrange
            var container = new TranslationsServiceContainer(true);
            var result = new RequestResult<TranslationResult>();
            var text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
            var translatedText = "If I knew Latin, maybe this translation would be correct";
            result.SetSuccess(new TranslationResult
            {
                Contents = new TranslationsResult_Contents
                {
                    Text = text,
                    Translated = translatedText,
                    Translation = "shakespeare"
                },
                Success = new TranslationResult_Success
                {
                    Total = 1
                }
            });
            
            container.RequestHandlerService.Setup(r => r.TrySendRequest<TranslationResult>(It.IsAny<string>(), HttpMethod.Post, null)).ReturnsAsync(result);
            container.RequestHandlerService.SetupGet(r => r.Settings).Returns(new Translations_ApiSettings());

            var translationResult = await container.TranslationsService.ToShakespearean(text);

            Assert.AreEqual(translatedText, translationResult);
        }
    }
}
