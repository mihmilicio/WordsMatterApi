import { Module } from '@nestjs/common';
import { ClientsModule, Transport } from '@nestjs/microservices';
import { RabbitMQService } from './rmq.service';
@Module({
  imports: [
    ClientsModule.register([
      {
        name: 'rabbit-mq-module',
        transport: Transport.RMQ,
        options: {
          urls: ['url_here'],
          queue: 'rabbit-mq-nest-js',
        },
      },
    ]),
  ],
  controllers: [],
  providers: [RabbitMQService],
  exports: [RabbitMQService],
})
export class RabbitMQModule {}
