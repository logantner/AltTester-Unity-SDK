package ro.altom.altunitytester;

import java.util.LinkedList;
import java.util.Queue;
import java.lang.Thread;

import javax.websocket.Session;
import com.google.gson.Gson;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;

import ro.altom.altunitytester.Notifications.AltUnityLoadSceneNotificationResultParams;
import ro.altom.altunitytester.Notifications.BaseNotificationCallbacks;
import ro.altom.altunitytester.Notifications.INotificationCallbacks;
import ro.altom.altunitytester.altUnityTesterExceptions.AltUnityErrors;
import ro.altom.altunitytester.altUnityTesterExceptions.AltUnityException;
import ro.altom.altunitytester.altUnityTesterExceptions.AltUnityInputModuleException;
import ro.altom.altunitytester.altUnityTesterExceptions.AssemblyNotFoundException;
import ro.altom.altunitytester.altUnityTesterExceptions.CameraNotFoundException;
import ro.altom.altunitytester.altUnityTesterExceptions.ComponentNotFoundException;
import ro.altom.altunitytester.altUnityTesterExceptions.CouldNotPerformOperationException;
import ro.altom.altunitytester.altUnityTesterExceptions.FailedToParseArgumentsException;
import ro.altom.altunitytester.altUnityTesterExceptions.FormatException;
import ro.altom.altunitytester.altUnityTesterExceptions.InvalidCommandException;
import ro.altom.altunitytester.altUnityTesterExceptions.InvalidParameterTypeException;
import ro.altom.altunitytester.altUnityTesterExceptions.InvalidPathException;
import ro.altom.altunitytester.altUnityTesterExceptions.MethodNotFoundException;
import ro.altom.altunitytester.altUnityTesterExceptions.MethodWithGivenParametersNotFoundException;
import ro.altom.altunitytester.altUnityTesterExceptions.NotFoundException;
import ro.altom.altunitytester.altUnityTesterExceptions.NullReferenceException;
import ro.altom.altunitytester.altUnityTesterExceptions.ObjectWasNotFoundException;
import ro.altom.altunitytester.altUnityTesterExceptions.PropertyNotFoundException;
import ro.altom.altunitytester.altUnityTesterExceptions.SceneNotFoundException;
import ro.altom.altunitytester.altUnityTesterExceptions.UnknownErrorException;

public class MessageHandler implements IMessageHandler {

    private Session session;
    private Queue<AltMessageResponse> responses = new LinkedList<AltMessageResponse>();
    private static final Logger logger = LogManager.getLogger(MessageHandler.class);
    private INotificationCallbacks notificationCallbacks;

    public INotificationCallbacks getNotificationCallbacks() {
        return notificationCallbacks;
    }

    public void setNotificationCallbacks(INotificationCallbacks notificationCallbacks) {
        this.notificationCallbacks = notificationCallbacks;
    }

    public MessageHandler(Session session) {
        this.session = session;
    }

    public <T> T receive(AltMessage data, Class<T> type) {
        while (responses.isEmpty() && session.isOpen()) {
            try {
                Thread.sleep(10);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        if (!session.isOpen()) {
            throw new AltUnityException("Driver disconnected");
        }
        AltMessageResponse responseMessage = responses.remove();

        T response = new Gson().fromJson(responseMessage.data, type);
        return response;

    }

    public void send(AltMessage altMessage) {
        String message = new Gson().toJson(altMessage);
        session.getAsyncRemote().sendText(message);
        logger.debug("command sent: {}", trimLogData(message));

    }

    public void onMessage(String message) {
        logger.debug("response received: {}", trimLogData(message));

        AltMessageResponse response = new Gson().fromJson(message, AltMessageResponse.class);
        handleErrors(response.error);

        if (response.isNotification) {
            handleNotification(response);
        } else {
            responses.add(response);
        }
    }

    private void handleNotification(AltMessageResponse message) {
        if (notificationCallbacks == null) {
            notificationCallbacks = new BaseNotificationCallbacks();
        }

        switch (message.commandName) {
        case "loadSceneNotification":
            AltUnityLoadSceneNotificationResultParams data = new Gson().fromJson(message.data,
                    AltUnityLoadSceneNotificationResultParams.class);
            notificationCallbacks.SceneLoadedCallBack(data);
            break;
        }
    }

    private void handleErrors(CommandError error) {
        if (error == null) {
            return;
        }

        switch (error.type) {
        case AltUnityErrors.errorNotFound:
            throw new NotFoundException(error.message);
        case AltUnityErrors.errorSceneNotFound:
            throw new SceneNotFoundException(error.message);
        case AltUnityErrors.errorPropertyNotFound:
            throw new PropertyNotFoundException(error.message);
        case AltUnityErrors.errorMethodNotFound:
            throw new MethodNotFoundException(error.message);
        case AltUnityErrors.errorComponentNotFound:
            throw new ComponentNotFoundException(error.message);
        case AltUnityErrors.errorAssemblyNotFound:
            throw new AssemblyNotFoundException(error.message);
        case AltUnityErrors.errorCouldNotPerformOperation:
            throw new CouldNotPerformOperationException(error.message);
        case AltUnityErrors.errorMethodWithGivenParametersNotFound:
            throw new MethodWithGivenParametersNotFoundException(error.message);
        case AltUnityErrors.errorFailedToParseArguments:
            throw new FailedToParseArgumentsException(error.message);
        case AltUnityErrors.errorInvalidParameterType:
            throw new InvalidParameterTypeException(error.message);
        case AltUnityErrors.errorObjectNotFound:
            throw new ObjectWasNotFoundException(error.message);
        case AltUnityErrors.errorPropertyNotSet:
            throw new PropertyNotFoundException(error.message);
        case AltUnityErrors.errorNullReference:
            throw new NullReferenceException(error.message);
        case AltUnityErrors.errorUnknownError:
            throw new UnknownErrorException(error.message);
        case AltUnityErrors.errorFormatException:
            throw new FormatException(error.message);
        case AltUnityErrors.errorInvalidPath:
            throw new InvalidPathException(error.message);
        case AltUnityErrors.errorInvalidCommand:
            throw new InvalidCommandException(error.message);
        case AltUnityErrors.errorInputModule:
            throw new AltUnityInputModuleException(error.message);
        case AltUnityErrors.errorCameraNotFound:
            throw new CameraNotFoundException(error.message);
        }

        logger.error(error.type + " is not handled by driver.");
        throw new UnknownErrorException(error.message);
    }

    private String trimLogData(String data) {
        return trimLogData(data, 1024 * 10);
    }

    private String trimLogData(String data, int maxSize) {
        if (data.length() > maxSize) {
            return data.substring(0, 10 * 1024) + "[...]";
        }
        return data;
    }

    private Type getType(Class<?> rawClass, Class<?> parameter) {
        return new ParameterizedType() {
            @Override
            public Type[] getActualTypeArguments() {
                return new Type[] { parameter };
            }

            @Override
            public Type getRawType() {
                return rawClass;
            }

            @Override
            public Type getOwnerType() {
                return null;
            }
        };
    }

}