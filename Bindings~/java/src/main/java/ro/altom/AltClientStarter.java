package ro.altom;

import java.net.URI;
import java.util.HashMap;

import javax.websocket.ContainerProvider;
import javax.websocket.Session;
import javax.websocket.WebSocketContainer;

// import org.eclipse.jetty.websocket.jsr356.ClientContainer;

import ro.altom.altunitytester.AltClientEndpoint;
import ro.altom.altunitytester.AltMessage;

public class AltClientStarter {

    public static void main(final String[] args) throws Exception {

        final WebSocketContainer container = ContainerProvider.getWebSocketContainer();
        final String uri = "ws://13000/altws/";

        try (Session session = container.connectToServer(AltClientEndpoint.class, URI.create(uri))) {
            session.getBasicRemote().sendObject(new AltMessage("commandName", new HashMap<String, Object>()));
        }

        // Application doesn't exit if container's threads are still running
        // ((ClientContainer) container).stop();
    }

}
