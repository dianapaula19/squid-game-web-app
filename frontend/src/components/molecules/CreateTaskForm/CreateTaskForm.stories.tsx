import React from 'react';
import { ComponentStory, ComponentMeta } from '@storybook/react';

import { CreateTaskForm } from './CreateTaskForm';

export default {
  title: 'Forms/CreateTaskForm',
  component: CreateTaskForm,
} as ComponentMeta<typeof CreateTaskForm>;

const Template: ComponentStory<typeof CreateTaskForm> = () => <CreateTaskForm/>;

export const Primary = Template.bind({});