import {
  Controller,
  Get,
  Post,
  Body,
  Put,
  Param,
  Delete,
} from '@nestjs/common';
import { SalaService } from './sala.service';
import { SalaDto } from './dto/sala.dto';
import { EventPattern } from '@nestjs/microservices';
import { VencedorDto } from './dto/vencedor.dto';

@Controller('sala')
export class SalaController {
  constructor(private readonly service: SalaService) {}

  @Post()
  create(@Body() dto: SalaDto) {
    return this.service.create(dto);
  }

  @Get()
  findAll() {
    return this.service.findAll();
  }

  @Get(':id')
  findOne(@Param('id') id: string) {
    return this.service.findOne(id);
  }

  @Put(':id')
  update(@Param('id') id: string, @Body() dto: SalaDto) {
    return this.service.update(id, dto);
  }

  @Delete(':id')
  remove(@Param('id') id: string) {
    return this.service.remove(id);
  }

  @EventPattern('vencedor_sala')
  async setVencedor(data: VencedorDto) {
    return this.service.setVencedor(data);
  }
}
