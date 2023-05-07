import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { SalaModule } from './sala/sala.module';
import { Sala } from './sala/entitites/sala.entity';

@Module({
  imports: [
    TypeOrmModule.forRoot({
      type: 'sqlite',
      database: 'database.db',
      entities: [Sala],
      synchronize: true,
    }),
    SalaModule,
  ],
})
export class AppModule {}
