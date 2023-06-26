import { Injectable, NotFoundException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Sala } from './entitites/sala.entity';
import { SalaDto } from './dto/sala.dto';
import { VencedorDto } from './dto/vencedor.dto';

@Injectable()
export class SalaService {
  constructor(
    @InjectRepository(Sala)
    private repository: Repository<Sala>,
  ) {}

  public async create(dto: SalaDto): Promise<Sala> {
    let sala = new Sala();
    sala = { ...sala, ...dto, id: this.getId() };
    return await this.repository.save(sala);
  }

  public async findAll(): Promise<Sala[]> {
    return await this.repository.find();
  }

  public async findOne(id: string): Promise<Sala> {
    const sala = await this.repository.findOneBy({ id });
    if (!sala) {
      throw new NotFoundException();
    }
    return sala;
  }

  public async update(id: string, dto: SalaDto): Promise<Sala> {
    let sala = await this.findOne(id);
    sala = { ...sala, ...dto };
    return await this.repository.save(sala);
  }

  public async remove(id: string): Promise<void> {
    const sala = await this.findOne(id);
    await this.repository.delete(sala);
    return;
  }

  public async setVencedor(dto: VencedorDto): Promise<Sala> {
    let sala = await this.findOne(dto.id);
    sala = { ...sala, vencedor: dto.nome };
    return await this.repository.save(sala);
  }

  private getId(): string {
    let result = '';
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    const charactersLength = characters.length;
    let counter = 0;
    while (counter < 6) {
      result += characters.charAt(Math.floor(Math.random() * charactersLength));
      counter += 1;
    }
    return result;
  }
}
