import { Injectable, Inject } from '@nestjs/common';
import { ClientProxy } from '@nestjs/microservices';

@Injectable()
export class RabbitMQService {
  constructor(
    @Inject('rabbit-mq-module') private readonly client: ClientProxy,
  ) {}

  public send(pattern: string, data: any) {
    return this.client.send(pattern, data).toPromise();

    // to send a message:
    // this.rabbitMQService.send('rabbit-mq-producer', {
    //   message: this.appService.getHello(),
    // });
  }
}
