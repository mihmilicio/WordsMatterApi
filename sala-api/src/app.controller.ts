import { Controller, Get } from '@nestjs/common';
import { RabbitMQService } from 'src/rmq/rmq.service';

@Controller('app')
export class AppController {
  constructor(private readonly rmq: RabbitMQService) {}

  @Get('teste-rmq')
  testeRMQ() {
    return this.rmq.send(
      '',
      JSON.stringify({
        Id: 2,
        Texto: 'ola',
      }),
    );
  }
}
