import React from 'react';
import { ComponentStory, ComponentMeta } from '@storybook/react';

import { Task } from './Task';

export default {
  title: 'Forms/Task',
  component: Task,
} as ComponentMeta<typeof Task>;

const Template: ComponentStory<typeof Task> = (args) => <Task {...args}/>;

export const Primary = Template.bind({});