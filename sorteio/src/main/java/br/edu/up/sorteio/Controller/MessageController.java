package br.edu.up.sorteio.Controller;

import io.nats.client.Nats;
import io.nats.client.Options;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;

import java.util.ArrayList;
import java.util.List;

import org.springframework.amqp.core.Message;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;
import io.nats.client.Connection;

import com.fasterxml.jackson.databind.ObjectMapper;

import br.edu.up.sorteio.Model.TextoEnviado;
import java.util.Random;

@RestController
@Api(tags = "Mensagens", description = "API para consumo de mensagens da fila RabbitMQ")
public class MessageController {

    private final ObjectMapper objectMapper;
    private final RabbitTemplate rabbitTemplate;
    private final JmsTemplate jmsTemplate;

    private final String natsUrl = "nats://localhost:4222"; // Substitua pelo URL do servidor NATS
    private final String natsSubject = "FILA_GANHADOR"; // Substitua pelo nome do tópico


    @Autowired
    public MessageController(RabbitTemplate rabbitTemplate,ObjectMapper objectMapper,JmsTemplate jmsTemplate) {
        this.rabbitTemplate = rabbitTemplate;
        this.objectMapper = objectMapper;
        this.jmsTemplate = jmsTemplate;
    }

    @GetMapping("/GerarSorteio")
    @ApiOperation("Consume as mensagens da fila")
    public void consumeMessages() {
        List<TextoEnviado> mensagens = new ArrayList<TextoEnviado>();
        try {
        Message message = rabbitTemplate.receive("EnvioParticipante");
        String mensagemAux = new String(message.getBody());
        while (mensagemAux != null) {
            TextoEnviado texto = objectMapper.readValue(mensagemAux,TextoEnviado.class);
            mensagens.add(texto);
            // Lógica para processar a mensagem recebida
            message = rabbitTemplate.receive("EnvioParticipante");
            if (message != null)
            {
                mensagemAux = new String(message.getBody());
            }
            else
            {
                mensagemAux = null;
            }
        }
        if (mensagens.size() != 0) 
        {
            Random gerador = new Random();
            int index = gerador.nextInt(mensagens.size());
            enviarMensagem(mensagens.get(index));
        }

        } catch (Exception e) {
            System.out.println("Erro ao processar a mensagem do RabbitMQ: " + e.getMessage());
        }
      
    }
    public String enviarMensagem(TextoEnviado textoEnviado) {
        Options options = new Options.Builder().server(natsUrl).build();
        try (Connection nc = Nats.connect(options)) {
            nc.publish(natsSubject, objectMapper.writeValueAsString(textoEnviado).getBytes());
        } catch (Exception e) {
            e.printStackTrace();
            return "Erro ao enviar mensagem: " + e.getMessage();
        }

        return "Mensagem enviada com sucesso";
    }


}