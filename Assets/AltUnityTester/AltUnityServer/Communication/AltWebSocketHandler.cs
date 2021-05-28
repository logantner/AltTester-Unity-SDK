using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Altom.AltUnityDriver;
using Altom.AltUnityDriver.Commands;
using Altom.Server.Logging;
using Assets.AltUnityTester.AltUnityServer.Commands;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Assets.AltUnityTester.AltUnityServer.Communication
{
    public class AltWebSocketHandler : WebSocketBehavior, ICommandHandler
    {
        private static readonly NLog.Logger logger = ServerLogManager.Instance.GetCurrentClassLogger();
        public static int MaxLengthLogMsg = 100;

        public AltWebSocketHandler()
        {
            logger.Debug("Client connected.");
        }

        private Type getCommandType(string commandName)
        {
            var assembly = Assembly.GetAssembly(typeof(CommandParams));

            var derivedType = typeof(CommandParams);
            var type = assembly.GetTypes().FirstOrDefault(t =>
               {
                   if (derivedType.IsAssignableFrom(t)) // if type derrives from CommandParams
                   {
                       CommandAttribute cmdAttribute = (CommandAttribute)Attribute.GetCustomAttribute(t, typeof(CommandAttribute));
                       return cmdAttribute != null && cmdAttribute.Name == commandName;
                   }
                   return false;
               });

            if (type == null) { throw new CommandNotFoundException(string.Format("Command `{0}` not found", commandName)); }
            return type;
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            logger.Debug("command received: " + e.Data);

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                Culture = CultureInfo.InvariantCulture
            };
            Func<string> executeAndSerialize = null;
            CommandParams cmdParams = null;

            try
            {
                cmdParams = JsonConvert.DeserializeObject<CommandParams>(e.Data, jsonSerializerSettings);
                var type = getCommandType((string)cmdParams.commandName);
                var commandParams = JsonConvert.DeserializeObject(e.Data, type, jsonSerializerSettings) as CommandParams;
                executeAndSerialize = createCommand(commandParams);
            }
            catch (JsonException ex)
            {
                executeAndSerialize = new AltUnityInvalidCommand(cmdParams, ex).ExecuteAndSerialize;
            }
            catch (CommandNotFoundException ex)
            {
                executeAndSerialize = new AltUnityInvalidCommand(cmdParams, ex).ExecuteAndSerialize;
            }

            AltUnityRunner._responseQueue.ScheduleResponse(delegate
               {
                   var response = executeAndSerialize();

                   this.Send(response);
                   logger.Debug("response sent: " + response);
               });
        }

        void ICommandHandler.Send(string data)
        {
            this.Send(data);
            logger.Debug("response sent: " + data);
        }

        private Func<string> createCommand(CommandParams cmdParams)
        {
            if (cmdParams is AltUnityGetServerVersionParams)
            {
                return new AltUnityGetServerVersionCommand((AltUnityGetServerVersionParams)cmdParams).ExecuteAndSerialize;
            }

            if (cmdParams is AltUnityTapObjectParams)
            {
                return new AltUnityTapCommand(cmdParams as AltUnityTapObjectParams).ExecuteAndSerialize;
            }

            if (cmdParams is AltUnityGetCurrentSceneParams)
            {
                return new AltUnityGetCurrentSceneCommand((AltUnityGetCurrentSceneParams)cmdParams).ExecuteAndSerialize;
            }

            if (cmdParams is AltUnityGetObjectComponentPropertyParams)
            {
                return new AltUnityGetComponentPropertyCommand(cmdParams as AltUnityGetObjectComponentPropertyParams).ExecuteAndSerialize;
            }

            if (cmdParams is AltUnitySetObjectComponentPropertyParams)
            {
                return new AltUnitySetObjectComponentPropertyCommand(cmdParams as AltUnitySetObjectComponentPropertyParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityCallComponentMethodForObjectParams)
            {
                return new AltUnityCallComponentMethodForObjectCommand(cmdParams as AltUnityCallComponentMethodForObjectParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityClickEventParams)
            {
                return new AltUnityClickEventCommand(cmdParams as AltUnityClickEventParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityTapScreenParams)
            {
                return new AltUnityClickOnScreenAtXyCommand(cmdParams as AltUnityTapScreenParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityTapCustomParams)
            {
                return new AltUnityClickOnScreenCustom(cmdParams as AltUnityTapCustomParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityDragObjectParams)
            {
                return new AltUnityDragObjectCommand(cmdParams as AltUnityDragObjectParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityPointerUpFromObjectParams)
            {
                return new AltUnityPointerUpFromObjectCommand(cmdParams as AltUnityPointerUpFromObjectParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityPointerDownFromObjectParams)
            {
                return new AltUnityPointerDownFromObjectCommand(cmdParams as AltUnityPointerDownFromObjectParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityPointerEnterObjectParams)
            {
                return new AltUnityPointerEnterObjectCommand(cmdParams as AltUnityPointerEnterObjectParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityPointerExitObjectParams)
            {
                return new AltUnityPointerExitObjectCommand(cmdParams as AltUnityPointerExitObjectParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityTiltParams)
            {
                return new AltUnityTiltCommand(cmdParams as AltUnityTiltParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityMultipointSwipeParams)
            {
                return new AltUnitySetMultipointSwipeCommand(cmdParams as AltUnityMultipointSwipeParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityMultipointSwipeChainParams)
            {
                return new AltUnitySetMultipointSwipeChainCommand(cmdParams as AltUnityMultipointSwipeChainParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityLoadSceneParams)
            {
                return new AltUnityLoadSceneCommand(this, (AltUnityLoadSceneParams)cmdParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityUnloadSceneParams)
            {
                return new AltUnityUnloadSceneCommand(this, cmdParams as AltUnityUnloadSceneParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnitySetTimeScaleParams)
            {
                return new AltUnitySetTimeScaleCommand(cmdParams as AltUnitySetTimeScaleParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetTimeScaleParams)
            {
                return new AltUnityGetTimeScaleCommand(cmdParams as AltUnityGetTimeScaleParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityDeletePlayerPrefParams)
            {
                return new AltUnityDeletePlayerPrefCommand(cmdParams as AltUnityDeletePlayerPrefParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityDeleteKeyPlayerPrefParams)
            {
                return new AltUnityDeleteKeyPlayerPrefCommand(cmdParams as AltUnityDeleteKeyPlayerPrefParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnitySetKeyPlayerPrefParams)
            {
                return new AltUnitySetKeyPlayerPrefCommand(cmdParams as AltUnitySetKeyPlayerPrefParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetKeyPlayerPrefParams)
            {
                var getKeyPlayerPrefParams = cmdParams as AltUnityGetKeyPlayerPrefParams;
                switch (getKeyPlayerPrefParams.keyType)
                {
                    case PlayerPrefKeyType.Int: return new AltUnityGetIntKeyPlayerPrefCommand(cmdParams as AltUnityGetKeyPlayerPrefParams).ExecuteAndSerialize;
                    case PlayerPrefKeyType.String: return new AltUnityGetStringKeyPlayerPrefCommand(cmdParams as AltUnityGetKeyPlayerPrefParams).ExecuteAndSerialize;
                    case PlayerPrefKeyType.Float: return new AltUnityGetFloatKeyPlayerPrefCommand(cmdParams as AltUnityGetKeyPlayerPrefParams).ExecuteAndSerialize;
                    default:
                        return new AltUnityInvalidCommand(cmdParams, new InvalidParameterTypeException(string.Format("PlayerPrefKeyType {0} not handled", getKeyPlayerPrefParams.keyType))).ExecuteAndSerialize;
                }
            }
            if (cmdParams is AltUnityActionFinishedParams)
            {
                return new AltUnityActionFinishedCommand(cmdParams as AltUnityActionFinishedParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetAllComponentsParams)
            {
                return new AltUnityGetAllComponentsCommand(cmdParams as AltUnityGetAllComponentsParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetAllFieldsParams)
            {
                return new AltUnityGetAllFieldsCommand(cmdParams as AltUnityGetAllFieldsParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetAllPropertiesParams)
            {
                return new AltUnityGetAllPropertiesCommand(cmdParams as AltUnityGetAllPropertiesParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetAllMethodsParams)
            {
                return new AltUnityGetAllMethodsCommand(cmdParams as AltUnityGetAllMethodsParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetAllScenesParams)
            {
                return new AltUnityGetAllScenesCommand(cmdParams as AltUnityGetAllScenesParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetAllCamerasParams)
            {
                return new AltUnityGetAllCamerasCommand(cmdParams as AltUnityGetAllCamerasParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetAllActiveCamerasParams)
            {
                return new AltUnityGetAllCamerasCommand(cmdParams as AltUnityGetAllActiveCamerasParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetAllLoadedScenesParams)
            {
                return new AltUnityGetAllLoadedScenesCommand(cmdParams as AltUnityGetAllLoadedScenesParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetAllLoadedScenesAndObjectsParams)
            {
                return new AltUnityGetAllLoadedScenesAndObjectsCommand(cmdParams as AltUnityGetAllLoadedScenesAndObjectsParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetScreenshotParams)
            {
                return new AltUnityGetScreenshotCommand(this, cmdParams as AltUnityGetScreenshotParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityHightlightObjectScreenshotParams)
            {
                return new AltUnityHighlightSelectedObjectCommand(this, cmdParams as AltUnityHightlightObjectScreenshotParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityHightlightObjectFromCoordinatesScreenshotParams)
            {
                return new AltUnityHightlightObjectFromCoordinatesCommand(this, cmdParams as AltUnityHightlightObjectFromCoordinatesScreenshotParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityPressKeyboardKeyParams)
            {
                return new AltUnityHoldButtonCommand(cmdParams as AltUnityPressKeyboardKeyParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityMoveMouseParams)
            {
                return new AltUnityMoveMouseCommand(cmdParams as AltUnityMoveMouseParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityScrollMouseParams)
            {
                return new AltUnityScrollMouseCommand(cmdParams as AltUnityScrollMouseParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityFindObjectParams)
            {
                return new AltUnityFindObjectCommand(cmdParams as AltUnityFindObjectParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityFindObjectsParams)
            {
                return new AltUnityFindObjectsCommand(cmdParams as AltUnityFindObjectsParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityFindObjectsLightParams)
            {
                return new AltUnityFindObjectsLightCommand(cmdParams as AltUnityFindObjectsLightParams).ExecuteAndSerialize;
            }
            // if (cmdParams is AltUnityfindActiveObjectByNameParams)
            // {
            //     return  new AltUnityFindActiveObjectsByNameCommand(cmdParams as CommandParams).ExecuteAndSerialize;
            // }

            if (cmdParams is AltUnityGetTextParams)
            {
                return new AltUnityGetTextCommand(cmdParams as AltUnityGetTextParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnitySetTextParams)
            {
                return new AltUnitySetTextCommand(cmdParams as AltUnitySetTextParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetPNGScreenshotParams)
            {
                return new AltUnityGetScreenshotPNGCommand(this, cmdParams as AltUnityGetPNGScreenshotParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnityGetServerVersionParams)
            {
                return new AltUnityGetServerVersionCommand(cmdParams as AltUnityGetServerVersionParams).ExecuteAndSerialize;
            }
            if (cmdParams is AltUnitySetServerLoggingParams)
            {
                return new AltUnitySetServerLoggingCommand(cmdParams as AltUnitySetServerLoggingParams).ExecuteAndSerialize;
            }

            return new AltUnityInvalidCommand(cmdParams, new CommandNotFoundException(string.Format("Command {0} not handled", cmdParams.commandName))).ExecuteAndSerialize;
        }
    }
}