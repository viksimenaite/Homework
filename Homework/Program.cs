using LiteDB;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;

namespace Homework
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserInterface.GreetTheUser();

            RunAsync().GetAwaiter().GetResult();

            Environment.Exit(0);
        }

        static async Task RunAsync()
        {
            ITesonetApi tesonetApi = new TesonetApi();

            ServerManager serverManager = new ServerManager(tesonetApi);

            TokensResponse token = await AuthenticationMenu(tesonetApi);

            await AuthorizedUserMenu(serverManager, token);
        }

        private static async Task AuthorizedUserMenu(ServerManager serverManager, TokensResponse token)
        {
            IServersRepository serversRepository = new ServersRepository();
            List<ServerResponse> serversList;

            bool isUserExited = false;
            while (!isUserExited)
            {
                switch (UserInterface.AskWhichListToGet())
                {
                    case 1:
                        serversList = await serverManager.GetAllRecordsAsync(token);

                        if (serversList != null)
                        {
                            UserInterface.DisplayListToConsole(serversList);
                            await serversRepository.StoreAllServersIntoDBAsync(serversList);
                        }
                        else
                        {
                            UserInterface.InformAboutAPIFailure();
                        }
                        break;
                    case 2:
                        serversList = await serversRepository.GetAllServersFromDBAsync();

                        if (serversList != null)
                        {
                            UserInterface.DisplayListToConsole(serversList);
                        }
                        else
                        {
                            UserInterface.InformAboutDBFailure();
                        }
                        break;
                    default:
                        UserInterface.InformAboutWrongInput();
                        break;
                }

                if (UserInterface.AskIfExit()) isUserExited = true;
            }
        }

        static async Task<TokensResponse> AuthenticationMenu(ITesonetApi api)
        {
            TokensResponse token = null;
            CredentialsPayload credentials = null;
            AuthenticationManager authManager = new AuthenticationManager(api);

            while (token == null)
            {
                credentials = UserInterface.AskForCredentials();

                token = await authManager.GetTokenAsync(credentials);

                if (token == null)
                {
                    UserInterface.InformAboutAuthorizationFailure();

                    if (UserInterface.AskIfExit()) break;
                }
            }

            return token;
        }
    }
}
