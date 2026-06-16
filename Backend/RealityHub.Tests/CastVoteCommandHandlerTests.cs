using Moq;
using RealityHub.Application.Interfaces;
using RealityHub.Application.UseCases.Votes.CastVote;
using RealityHub.Domain.Entities;

namespace RealityHub.Tests;

public class CastVoteCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Register_Vote_When_Valid()
    {
        // Arrange (preparação)
        var userId = Guid.NewGuid();
        var roundId = Guid.NewGuid();
        var participantId = Guid.NewGuid();

        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock
            .Setup(x => x.GetUserId())
            .Returns(userId);

        var requestMetadataServiceMock = new Mock<IRequestMetadataService>();
        requestMetadataServiceMock
            .Setup(x => x.GetIpAddress())
            .Returns("127.0.0.1");

        requestMetadataServiceMock
            .Setup(x => x.GetUserAgent())
            .Returns("UnitTest");

        var roundRepositoryMock = new Mock<IRoundRepository>();
        roundRepositoryMock
            .Setup(x => x.GetCurrentOpenRoundAsync())
            .ReturnsAsync(new Round
            {
                Id = roundId,
                Title = "Round Test",
                StartAt = DateTime.UtcNow.AddMinutes(-10),
                EndsAt = DateTime.UtcNow.AddDays(1),
            });

        var participantRepositoryMock = new Mock<IParticipantRepository>();
        participantRepositoryMock
            .Setup(x => x.GetByIdAsync(participantId))
            .ReturnsAsync(new Participant
            {
                Id = participantId,
                Name = "Participant Test",
                IsActive = true
            });

        var roundParticipantRepositoryMock = new Mock<IRoundParticipantRepository>();
        roundParticipantRepositoryMock
            .Setup(x => x.ExistisAsync(roundId, participantId))
            .ReturnsAsync(true);

        var voteRepositoryMock = new Mock<IVoteRepository>();
        voteRepositoryMock
            .Setup(x => x.GetLastVoteTimeAsync(userId, roundId))
            .ReturnsAsync((DateTime?)null);

        var voteAttemptLogRepositoryMock = new Mock<IVoteAttemptLogRepository>();

        var handler = new CastVoteCommandHandler(
            currentUserServiceMock.Object,
            requestMetadataServiceMock.Object,
            roundRepositoryMock.Object,
            participantRepositoryMock.Object,
            roundParticipantRepositoryMock.Object,
            voteRepositoryMock.Object,
            voteAttemptLogRepositoryMock.Object
        );

        var command = new CastVoteCommand
        {
            ParticipantId = participantId,
        };

        // Act (executar)
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert (verificar)
        Assert.True(result.Success);
        Assert.Equal("Voto registrado com sucesso.", result.Message);

        voteRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Vote>()), Times.Once);
        voteAttemptLogRepositoryMock.Verify(x => x.AddAsync(It.IsAny<VoteAttemptLog>()), Times.Once);

    }

    [Fact]
    public async Task Handle_Should_Block_Vote_when_Cooldown_Is_Active()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var roundId = Guid.NewGuid();   
        var participantId = Guid.NewGuid();

        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(x => x.GetUserId()).Returns(userId);

        var requestMetadataServiceMock = new Mock<IRequestMetadataService>();
        requestMetadataServiceMock.Setup(x => x.GetIpAddress()).Returns("127.0.0.1");
        requestMetadataServiceMock.Setup(x => x.GetUserAgent()).Returns("UnitTest");

        var roundRepositoryMock = new Mock<IRoundRepository>();
        roundRepositoryMock.Setup(x => x.GetCurrentOpenRoundAsync()).ReturnsAsync(new Round { Id = roundId });

        var participantRepositoryMock = new Mock<IParticipantRepository>();
        participantRepositoryMock.Setup(x => x.GetByIdAsync(participantId)).ReturnsAsync(new Participant {  Id = participantId, IsActive = true});

        var roundParticipantRepositoryMock = new Mock<IRoundParticipantRepository>();
        roundParticipantRepositoryMock.Setup(x => x.ExistisAsync(roundId, participantId)).ReturnsAsync(true);

        var voteRepositoryMock = new Mock<IVoteRepository>();

        // Simula que  o usuário votou há 30 segundos
        voteRepositoryMock.Setup(x => x.GetLastVoteTimeAsync(userId, roundId)).ReturnsAsync(DateTime.UtcNow.AddSeconds(-30));

        var voteAttemptLogRepositoryMock = new Mock<IVoteAttemptLogRepository>();

        var handler = new CastVoteCommandHandler
            (
                currentUserServiceMock.Object,
                requestMetadataServiceMock.Object,
                roundRepositoryMock.Object,
                participantRepositoryMock.Object,
                roundParticipantRepositoryMock.Object,
                voteRepositoryMock.Object,
                voteAttemptLogRepositoryMock.Object
            );

        var command = new CastVoteCommand
        {
            ParticipantId = participantId,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Você deve esperar antes de votar novamente.", result.Message);
        Assert.NotNull(result.SecondsToWait);

        // Não deve salvar voto
        voteRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Vote>()), Times.Never);

        // Deve registrar tentativa no log
        voteAttemptLogRepositoryMock.Verify(x => x.AddAsync(It.IsAny<VoteAttemptLog>()), Times.Once);
    }
}
