import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Sala } from './entitites/sala.entity';
import { SalaController } from './sala.controller';
import { SalaService } from './sala.service';

@Module({
  imports: [TypeOrmModule.forFeature([Sala])],
  controllers: [SalaController],
  providers: [SalaService],
})
export class SalaModule {}
