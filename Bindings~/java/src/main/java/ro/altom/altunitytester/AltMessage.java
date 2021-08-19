package ro.altom.altunitytester;

import javax.websocket.Encoder;
import javax.websocket.EndpointConfig;
import javax.websocket.DecodeException;
import javax.websocket.Decoder;
import javax.websocket.EncodeException;

import java.util.HashMap;

import com.google.gson.Gson;

public class AltMessage {

    private String commandName;
    private HashMap<String, Object> commandParams = new HashMap<String, Object>();

    public AltMessage(String commandName, HashMap<String, Object> commandParams) {
        this.commandName = commandName;
        this.commandParams.putAll(commandParams);
    }

    public AltMessage() {
    }

    public String getCommandName() {
        return commandName;
    }

    public HashMap<String, Object> getCommandParams() {
        return this.commandParams;
    }

    public static class AltMessageEncoder implements Encoder.Text<AltMessage> {
        @Override
        public void init(final EndpointConfig config) {
        }

        @Override
        public String encode(final AltMessage altMessage) throws EncodeException {
            return new Gson().toJson(altMessage);
        }

        @Override
        public void destroy() {
        }
    }

    public static class AltMessageDecoder implements Decoder.Text<AltMessage> {
        @Override
        public void init(final EndpointConfig config) {
        }

        @Override
        public AltMessage decode(final String str) throws DecodeException {
            return new Gson().fromJson(str, AltMessage.class);
        }

        @Override
        public boolean willDecode(final String str) {
            return true;
        }

        @Override
        public void destroy() {
        }
    }
}
