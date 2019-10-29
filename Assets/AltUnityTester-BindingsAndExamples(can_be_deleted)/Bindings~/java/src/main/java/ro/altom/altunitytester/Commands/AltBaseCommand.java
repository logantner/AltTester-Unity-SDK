package ro.altom.altunitytester.Commands;

import ro.altom.altunitytester.AltBaseSettings;
import ro.altom.altunitytester.AltUnityDriver;
import ro.altom.altunitytester.altUnityTesterExceptions.*;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;

public class AltBaseCommand {

    protected static final org.slf4j.Logger log = org.slf4j.LoggerFactory.getLogger(AltBaseCommand.class);

    private final static int BUFFER_SIZE = 1024;
    public AltBaseSettings altBaseSettings;

    public AltBaseCommand(AltBaseSettings altBaseSettings) {
        this.altBaseSettings = altBaseSettings;
    }

    protected String recvall() {
        String receivedData = "";
        boolean streamIsFinished = false;

        while (!streamIsFinished) {
            byte[] messageByte = new byte[BUFFER_SIZE];
            int bytesRead = 0;
            try {
                bytesRead = altBaseSettings.in.read(messageByte);
            } catch (IOException e) {
                throw new ConnectionException(e);
            }
            if (bytesRead > 0)
                receivedData += new String(messageByte, 0, bytesRead);
            if (receivedData.contains("::altend")) {
                streamIsFinished = true;
            }
        }

        receivedData = receivedData.split("altstart::")[1].split("::altend")[0];
        String[] data=receivedData.split("::altDebug::");
        receivedData=data[0];
        log.debug("Data received: " + receivedData);
        if(altBaseSettings.debugEnabled)
            WriteInDebugFile(data[1]);
        return receivedData;
    }

    private void WriteInDebugFile(String debugMessages){
        BufferedWriter writer = null;
        try {
            writer = new BufferedWriter(new FileWriter("LogAltUnityFile", true));
            writer.append(debugMessages);
            writer.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    protected void send(String message) {
        log.info("Sending rpc message [{}]", message);
        altBaseSettings.out.print(message);
        altBaseSettings.out.flush();
    }

    protected String CreateCommand(String... arguments){
        String command="";
        for (String argument:arguments) {
            command+=argument+altBaseSettings.RequestSeparator;
        }
        return command+altBaseSettings.RequestEnd;

    }

    protected void handleErrors(String data) {
        String typeOfException = data.split(";")[0];
        if ("error:notFound".equals(typeOfException)) {
            throw new NotFoundException(data);
        } else if ("error:propertyNotFound".equals(typeOfException)) {
            throw new PropertyNotFoundException(data);
        } else if ("error:methodNotFound".equals(typeOfException)) {
            throw new MethodNotFoundException(data);
        } else if ("error:componentNotFound".equals(typeOfException)) {
            throw new ComponentNotFoundException(data);
        } else if ("error:couldNotPerformOperation".equals(typeOfException)) {
            throw new CouldNotPerformOperationException(data);
        } else if ("error:couldNotParseJsonString".equals(typeOfException)) {
            throw new CouldNotParseJsonStringException(data);
        } else if ("error:incorrectNumberOfParameters".equals(typeOfException)) {
            throw new IncorrectNumberOfParametersException(data);
        } else if ("error:failedToParseMethodArguments".equals(typeOfException)) {
            throw new FailedToParseArgumentsException(data);
        } else if ("error:objectNotFound".equals(typeOfException)) {
            throw new ObjectWasNotFoundException(data);
        } else if ("error:propertyCannotBeSet".equals(typeOfException)) {
            throw new PropertyNotFoundException(data);
        } else if ("error:nullReferenceException".equals(typeOfException)) {
            throw new NullReferenceException(data);
        } else if ("error:unknownError".equals(typeOfException)) {
            throw new UnknownErrorException(data);
        } else if ("error:formatException".equals(typeOfException)) {
            throw new FormatException(data);
        }
    }

    public String vectorToJsonString(int x, int y) {
        return "{\"x\":" + x + ", \"y\":" + y + "}";
    }

    public String vectorToJsonString(int x, int y, int z) {
        return "{\"x\":" + x + ", \"y\":" + y + ", \"z\":" + z + "}";
    }

    /**
     * Sleeps for certain amount of seconds.
     *
     * @param interval Seconds to sleep for.
     */
    protected void sleepFor(double interval) {
        long timeToSleep = (long) (interval * 1000);
        try {
            Thread.sleep(timeToSleep);
        } catch (InterruptedException e) {
            log.warn("Could not sleep for " + timeToSleep + " ms");
        }
    }
}
