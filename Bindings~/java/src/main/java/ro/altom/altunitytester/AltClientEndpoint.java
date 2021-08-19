package ro.altom.altunitytester;

import java.io.IOException;
import java.util.HashMap;
import java.util.logging.Logger;

import javax.websocket.ClientEndpoint;
import javax.websocket.OnMessage;
import javax.websocket.OnOpen;
import javax.websocket.Session;
import javax.websocket.EncodeException;

import ro.altom.altunitytester.AltMessage.AltMessageEncoder;
import ro.altom.altunitytester.AltMessage.AltMessageDecoder;

@ClientEndpoint(encoders = { AltMessageEncoder.class }, decoders = { AltMessageDecoder.class })
public class AltClientEndpoint {
    private static final Logger log = Logger.getLogger(AltClientEndpoint.class.getName());

    @OnOpen
    public void onOpen(final Session session) throws IOException, EncodeException {
        session.getBasicRemote().sendObject(new AltMessage("commandName", new HashMap<String, Object>()));
    }

    @OnMessage
    public void onMessage(final AltMessage altMessage) {
        log.info(String.format("Received message '%s' from '%s'", altMessage.getCommandName(), "commandParams"));
    }
}
