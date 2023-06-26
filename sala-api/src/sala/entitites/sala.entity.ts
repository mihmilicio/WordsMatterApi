import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity()
export class Sala {
  @PrimaryColumn({ length: 6 })
  id: string;

  @Column()
  nome: string;

  @Column()
  tema: string;

  @Column()
  maxEnvios?: number;

  @Column({ nullable: true })
  vencedor?: string;
}
