## This is a WRAPPER project. You can find the base library [here](https://github.com/DSharpPlus/DSharpPlus).

![CodeSize](https://img.shields.io/github/languages/code-size/VelvetThePanda/DSharpPlus.Abstractions)
![Lines of code](https://img.shields.io/tokei/lines/github/VelvetThePanda/DSharpPlus.Abstractions)

## What is DSharpPlus.Abstractions?
Good question, and the answer is simple:

This is a wrapper library that aims to be as minimal as possible; we only wrap REST entities.

What is a REST entity? Anything in D#+ that has a method that invokes a method in the `DiscordApiClient` will be wrapped in an abstraction for mocking purposes.

Notable examples of this are:
- DiscordClient
- DiscordMessage
- DiscordChannel
- DiscordUser
- DiscordMember

And many more. Given that this library aims to be a *wrapper*, there is a dependency on [DSharpPlus](https://github.com/DSharpPlus/DSharpPlus). 

Anything that has access to Discord's API* will be wrapped in an interface, if possible. Examples include, but are not limited to:

- DiscordClient -> IDiscordClient
- DiscordMessage -> IMessage
- DiscordChannel -> IChannel
- DiscordUser -> IUser

These abstractions actually hold references to D#+ entities under the hood, and will (in their default implementation) invoke D#+ methods.


